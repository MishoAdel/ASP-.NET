using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using StocksApp.Models;
using System.Threading.Tasks;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IStockSrevice _stockSrevice;
        private readonly IConfiguration _configuration;
        private readonly TradingOptions _tradingOptions;
        private readonly ILogger<TradeController> _logger;


        public TradeController(IFinnhubService finnhubService, IStockSrevice stockSrevice,IConfiguration configuration, IOptions<TradingOptions> tradingOptions, ILogger<TradeController> logger)
        {
            _finnhubService = finnhubService;
            _stockSrevice = stockSrevice;
            _configuration = configuration;
            _tradingOptions = tradingOptions.Value;
            _logger = logger;
        }

        [Route("[action]/{stockSymbol}")]
        [Route("~/[controller]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            _logger.LogInformation("In TradeController.Index() action method");
            _logger.LogDebug("stockSymbol: {stockSymbol}", stockSymbol);

            if (string.IsNullOrEmpty(stockSymbol))
                stockSymbol = "MSFT";

            Dictionary<string, object>? companyProfileDictionary = await _finnhubService.GetCompanyProfile(stockSymbol);

            Dictionary<string,object>? stockPriceQuoteDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockSymbol = stockSymbol,
            };

            if (companyProfileDictionary != null && stockPriceQuoteDictionary != null)
            {
                stockTrade = new StockTrade()
                {
                    StockSymbol = companyProfileDictionary["ticker"].ToString(),
                    StockName = companyProfileDictionary["name"].ToString(),
                    Price = Convert.ToDouble(stockPriceQuoteDictionary["c"].ToString()),
                    Quantity = _tradingOptions.DefaultOrderQuantity ?? 0
                };
            }

            ViewBag.FinnhubToken = _configuration["api-token"];

            return View(stockTrade);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> BuyOrderAsync(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(buyOrderRequest);


            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = buyOrderRequest.StockName, Quantity = buyOrderRequest.Quantity, StockSymbol = buyOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            BuyOrderResponse buyOrderResponse = await _stockSrevice.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SellOrderAsync(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(sellOrderRequest);


            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            SellOrderResponse sellOrderResponse = await _stockSrevice.CreateSellOrder(sellOrderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrderResponses = await _stockSrevice.GetBuyOrders();

            List<SellOrderResponse> sellOrderResponses = await _stockSrevice.GetSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };

            ViewBag.TradingOptions = _tradingOptions;

            return View(orders);
        }

        [Route("OrdersPDF")]
        public async Task<IActionResult> OrdersPDF()
        {
            throw new NotImplementedException();
        }


    }
}
