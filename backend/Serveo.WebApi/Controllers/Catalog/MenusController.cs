using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Features;
using Serveo.Application.Features.Catalog.Categories.Create;
using Serveo.Application.Features.Catalog.Menus.Create;
using Serveo.Application.Features.Catalog.Menus.Get;
using Serveo.Application.Features.Catalog.Menus.Items.Create;
using Serveo.Application.Features.Catalog.Products.Create;
using Serveo.Application.Services;
using Serveo.WebApi.Common;
using Serveo.WebApi.Extensions;
using Serveo.WebApi.Models;
using Serveo.WebApi.Models.Catalog.Categories;
using Serveo.WebApi.Models.Catalog.Memus;
using Serveo.WebApi.Models.Catalog.Products;
using Serveo.WebApi.Models.Tenanting.Outlets;

namespace Serveo.WebApi.Controllers.Admin.Catalog
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/menus")]
    [ApiController]
    [Tags(ApiTags.Menus)]
    public class MenusController(IMediator mediator, PayloadMapper mapper) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType<PagedResult<MenuDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int page, int size, CancellationToken ct)
        {
            if (page < 1) page = 1;
            if (size < 1) size = 10;

            var result = await mediator.SendAsync(new PageMenuCommand(new PageQuery(page, size)), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateMenuRequest req, CancellationToken ct)
        {
            if (!User.TryGetBusinessId(out var businessId))
                businessId = req.BusinessId;

            var result = await mediator.SendAsync(new CreateMenuCommand(businessId ?? Guid.Empty, req.Name, req.Description), ct);

            return this.ToActionResult(result, x => Ok(mapper.ToResponse(x)));
        }

        [HttpPost("{id}/items")]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Items(Guid id, [FromBody] CreateMenuItemRequest req, CancellationToken ct)
        {
            var result = await mediator.SendAsync(new CreateMenuItemCommand(id, req.ProductIds), ct);

            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<PagedResult<MenuDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Details(Guid id, bool full, CancellationToken ct)
        {
            var result = await mediator.SendAsync(new MenuDetailsCommand(id, full), ct);

            return this.ToActionResult(result);
        }

        #region Menu -> Category
        // Tạo mới một danh mục thuộc menu
        [HttpPost("{id}/categories")]
        public async Task<IActionResult> CreateCategoryAsync(Guid id, [FromBody] CreateCategoryRequest req, CancellationToken ct)
        {
            if (!User.TryGetBusinessId(out var businessId))
                businessId = req.BusinessId;

            var result = await mediator.SendAsync(new CreateCategoryCommand(businessId ?? Guid.Empty, req.Name, id), ct);

            return this.ToActionResult(result, x => Created("", mapper.ToResponse(x)));
        }

        // Gắn một category đã tồn tại vào menu
        [HttpPost("{id}/categories/{categoryId}")]
        public IActionResult AtachCategory(Guid id, CreateCategoryRequest req)
        {
            return Ok();
        }

        // Gắn nhiều category đã tồn tại vào menu
        [HttpPost("{id}/categories/atach")]
        public IActionResult AtachCategories(Guid id, CreateCategoryRequest req)
        {
            return Ok();
        }

        // Gỡ hàng loạt (Bulk Detach) nhiều Category cùng lúc
        [HttpPost("{id}/categories/detach")]
        public IActionResult DetachCategories(Guid id, Guid categoryId)
        {
            return Ok();
        }

        // Gỡ tất cả category
        [HttpDelete("{id}/categories/{categoryId}")]
        public IActionResult DeleteCategory(Guid id, Guid categoryId)
        {
            return Ok();
        }

        // Gỡ 1 Category khỏi Menu
        [HttpDelete("{id}/categories")]
        public IActionResult DeleteCategories(Guid id, Guid categoryId)
        {
            return Ok();
        }

        [HttpPost("{id}/categories/{categoryId}/order")]
        public IActionResult UpdateCategoryOrder(Guid id, Guid categoryId, int order)
        {
            return Ok();
        }
        /*
         MENU
GET /api/menus/{menuId}


MENU → CATEGORY
POST   /api/menus/{menuId}/categories
DELETE /api/menus/{menuId}/categories/{categoryId}
PUT    /api/menus/{menuId}/categories/order


MENU → PRODUCT
POST   /api/menus/{menuId}/items
DELETE /api/menus/{menuId}/items/{productId}
PUT    /api/menus/{menuId}/items/order


CATALOG
GET    /api/categories
POST   /api/categories
PATCH  /api/categories/{categoryId}
DELETE /api/categories/{categoryId}

GET    /api/products
POST   /api/products
PATCH  /api/products/{productId}
DELETE /api/products/{productId}
         */
        #endregion

        #region Menu -> Product
        // Create a new product for the menu
        [HttpPost("{id}/products")]
        public async Task<IActionResult> CreateProduct(Guid id, [FromBody] CreateProductRequest req, CancellationToken ct)
        {
            if (!User.TryGetBusinessId(out var businessId))
                businessId = req.BusinessId;

            var command = mapper.ToCommand(req);
            command.BusinessId = businessId;
            command.MenuId = id;

            var result = await mediator.SendAsync(command, ct);

            return this.ToActionResult(result);
        }
        #endregion
    }
}
