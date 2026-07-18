using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features.Catalog.Menus.Get;
using Serveo.Application.Features.Catalog.Products.Get;
using Serveo.Application.Features.Ordering.QR;
using Serveo.Application.Features.Tenanting.RegisterTenant;
using Serveo.Infrastructure.Authentication.ApiKey;
using Serveo.WebApi.Models.Public;

namespace Serveo.WebApi.Controllers.Public
{
    [Authorize(AuthenticationSchemes = $"{ApiKeyAuthenticationOptions.AuthenticationScheme}")]
    [Route("api/public")]
    [ApiController]
    [Tags(ApiTags.Public)]
    public class PublicController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("tenants/register")]
        public async Task<IActionResult> RegisterTenant([FromBody] RegisterTenantRequest req, CancellationToken ct)
        {
            var command = mapper.Map<RegisterTenantCommand>(req);
            var result = await _mediator.SendAsync(command, ct);
            return Ok(result);
        }

        // https://chatgpt.com/c/6a421856-923c-83ec-8524-2250ae9ab37f?mweb_fallback=1
        // App Initialization
        // Sau khi quét QR
        // API này chỉ gọi 1 lần.
        [AllowAnonymous]
        [HttpGet("init")]
        [ProducesResponseType<InitializationResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Init(string token, CancellationToken ct)
        {
            var commandResult = await _mediator.SendAsync(new ScanCommand(token), ct);
            var response = new InitializationResponse
            {
                Business = _mapper.Map<InitBusinessResponse>(commandResult.Table.Branch?.Business),
                Branch = _mapper.Map<InitBranchResponse>(commandResult.Table.Branch),
                Table = _mapper.Map<InitTableResponse>(commandResult.Table),
                Theme = commandResult.Theme
            };

            return Ok(response);
        }

        // Lấy Menu
        // Có cache rất mạnh.
        [HttpGet("menu")]
        public async Task<IActionResult> Menu(CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new PublicMenuCommand(Guid.Parse("ffd1a803-5541-4eb4-b46c-08ded4f3c46f")), ct);
            return Ok(result);
        }

        // Product Detail
        // Nếu menu không trả hết topping
        // Trả: Variant, Option Group, Topping, Image, Description
        [HttpGet("products/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id, CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new PublicProductCommand(id), ct);
            return Ok(result);
        }

        // Search Product
        //[HttpGet("products")]
        //public async Task<IActionResult> Products(Guid id, CancellationToken ct)
        //{
        //    var result = "[\r\n    {\r\n        \"id\":\"coffee\",\r\n        \"name\":\"Coffee\",\r\n        \"products\":[]\r\n    }\r\n]";
        //    return Ok(result);
        //}

        // https://chatgpt.com/g/g-p-6a29780664648191a9f08f561a8183e2-serveo-saas/c/6a472106-1b74-83ec-89fd-a08d03041cb3
        // Order Session
        // Ngay khi mở app
        // Dùng để: cart, reconnect, SignalR
        [HttpPost("order-session")]
        public async Task<IActionResult> OrderSession(CancellationToken ct)
        {
            var result = "sessionId";
            return Ok(result);
        }

        // Create Order
        [HttpPost("orders")]
        public async Task<IActionResult> Orders(CancellationToken ct)
        {
            var body = "{\r\n  \"sessionId\":\"...\",\r\n  \"items\":[]\r\n}";
            var result = "{\r\n    \"orderId\":\"...\",\r\n    \"orderNumber\":\"A0001\",\r\n    \"status\":\"Pending\"\r\n}";
            return Ok(result);
        }

        // Order Detail
        // Trả: items, total, status, payment
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> Orders(Guid id, CancellationToken ct)
        {
            var result = "items, total, status, payment";
            return Ok(result);
        }

        // Order Status
        // Có thể polling hoặc SignalR.
        [HttpGet("orders/{id}/status")]
        public async Task<IActionResult> OrderStatus(Guid id, CancellationToken ct)
        {
            var result = "";
            return Ok(result);
        }

        [HttpGet("payment-methods")]
        public async Task<IActionResult> PaymentMethods(CancellationToken ct)
        {
            var result = "Cash, VNPay, MoMo, Stripe";
            return Ok(result);
        }

        // Create Payment
        [HttpPost("orders/{id}/payment")]
        public async Task<IActionResult> CreatePayment(Guid id, CancellationToken ct)
        {
            var result = "paymentUrl";
            return Ok(result);
        }

        // Payment Status
        [HttpGet("payments/{id}")]
        public async Task<IActionResult> Payments(Guid id, CancellationToken ct)
        {
            var result = "paymentUrl";
            return Ok(result);
        }

        // Feedback
        [HttpPost("feedback/{id}")]
        public async Task<IActionResult> Feedback(CancellationToken ct)
        {
            var result = "⭐⭐⭐⭐⭐ Ngon";
            return Ok(result);
        }

        // Request Bill
        [HttpPost("waiter-call")]
        public async Task<IActionResult> WaiterCall(CancellationToken ct)
        {
            var body = "{\r\n    \"tableId\":\"...\"\r\n}";
            return Ok(body);
        }

        // Request Bill
        [HttpPost("request-bill")]
        public async Task<IActionResult> RequestBill(CancellationToken ct)
        {
            var body = "{\r\n    \"tableId\":\"...\"\r\n}";
            return Ok(body);
        }

        // Restaurant Info
        [HttpGet("business")]
        public async Task<IActionResult> Business(CancellationToken ct)
        {
            var result = "Logo, Tên, Mở cửa, Điện thoại, Địa chỉ";
            return Ok(result);
        }

        // Promotions
        [HttpGet("promotions")]
        public async Task<IActionResult> Promotions(CancellationToken ct)
        {
            var result = "Logo, Tên, Mở cửa, Điện thoại, Địa chỉ";
            return Ok(result);
        }

        // Banner
        [HttpGet("banners")]
        public async Task<IActionResult> Banners(CancellationToken ct)
        {
            var result = "Logo, Tên, Mở cửa, Điện thoại, Địa chỉ";
            return Ok(result);
        }
    }


}

/**************************************************************
 * https://chatgpt.com/g/g-p-6a29780664648191a9f08f561a8183e2-serveo-saas/c/6a41db9a-9d40-83ec-a49b-71189237de99
 */
/*
Kiến trúc đề xuất cho Serveo
/api/public
    GET    /init
    GET    /menu
    GET    /products/{id}
    POST   /order-session
    POST   /orders
    GET    /orders/{id}
    GET    /orders/{id}/status
    GET    /payment-methods
    POST   /orders/{id}/payment
    POST   /waiter-call
    POST   /request-bill
    GET    /business
    GET    /promotions
    GET    /banners

/api/admin
    ...

/api/staff
    ...

/api/auth
    ...
     */

/*
 API tối thiểu (MVP)
| API                              | Bắt buộc                 |
| -------------------------------- | ------------------------ |
| GET /public/init                 | ✅                        |
| GET /public/menu                 | ✅                        |
| GET /public/products/{id}        | ✅                        |
| POST /public/order-session       | ✅                        |
| POST /public/orders              | ✅                        |
| GET /public/orders/{id}          | ✅                        |
| GET /public/orders/{id}/status   | ✅                        |
| GET /public/payment-methods      | Nếu có thanh toán online |
| POST /public/orders/{id}/payment | Nếu có thanh toán online |
| POST /public/request-bill        | Nhà hàng phục vụ tại bàn |
| POST /public/waiter-call         | Nhà hàng phục vụ tại bàn |
| GET /public/business             | Hiển thị thông tin quán  |
| GET /public/promotions           | Tùy chọn                 |
| GET /public/banners              | Tùy chọn                 |

 */
