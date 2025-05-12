using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedLayer.Dtos;
using SharedLayer.Options;
using UniSystem.Core.PaymentModels;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly IyzicoOptions _opt;

        public PaymentController(IOptions<IyzicoOptions> opt)
        {
            _opt = opt.Value;
        }
        [HttpPost]
        public async Task<IActionResult> Pay(PaymentRequest req)
        {
            Iyzipay.Options options = new Iyzipay.Options()
            {
                ApiKey = _opt.ApiKey,
                BaseUrl = _opt.BaseUrl,
                SecretKey = _opt.SecretKey,
            };
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "123";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://localhost:7170/api/Payment/PayCallBack";

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "accessor";
            firstBasketItem.Price = "123";
            firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            basketItems.Add(firstBasketItem);


            request.BasketItems = basketItems;
            ThreedsInitialize threedsInitialize = ThreedsInitialize.Create(request, options);

            //return await Task.FromResult(Ok(new { Content = threedsInitialize.HtmlContent }));
            return Ok(new { Content = threedsInitialize.HtmlContent });
        }


        [HttpPost]
        public IActionResult PayCallBack([FromForm] CallBackData request)
        {
            if (request.status == "success")
            {
                return Ok("ÖDEME İŞLEMİ BAŞARILI İLE GERCEKLEŞTİ");
            }
            return BadRequest("ÖDEME YAPILAMADI...");
        }
    }
}
