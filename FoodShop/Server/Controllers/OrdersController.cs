using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace FoodShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // get all orders
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Order>>> GetOrders()
        {
            var result = await _orderService.GetOrdersAsync();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // create order POST
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Order>>> CreateOrder()
        {
            var result = await _orderService.CreateOrderAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // delete order DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteOrder(string id)
        {
            var result = await _orderService.DeleteOrderAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // get order by id GET
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Order>>> GetOrder(string id)
        {
            var result = await _orderService.GetOrderAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // update order PUT
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Order>>> UpdateOrder(OrderUpdateVM request)
        {
            var result = await _orderService.UpdateOrderAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpGet("export/csv")]
        //public async Task<IActionResult> ExportToCSV()
        //{
        //    var products = await _orderService.GetOrdersAsync();
            
        //    var builder = new StringBuilder();

        //    builder.AppendLine("Id, Created at, Total Price, Status");

        //    foreach (var item in products.Data)
        //    {
        //        builder.AppendLine($"{item.Id},{item.CreatedAt},{item.TotalPrice},{item.OrderStatus}");
        //    }

        //    return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "order.csv");
        //}

        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var products = await _orderService.GetOrdersAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Orders");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Created at";
                worksheet.Cell(currentRow, 3).Value = "Items";
                worksheet.Cell(currentRow, 4).Value = "Total Price";
                worksheet.Cell(currentRow, 5).Value = "Status";

                foreach (var item in products.Data)
                {
                    currentRow++;
                    string itemOrder = "";

                    item.OrderItems.ForEach(i =>
                    {
                        itemOrder += $"| {i.Product.Name} x {i.Qty} ";
                    });
                    
                    worksheet.Cell(currentRow, 1).Value = item.Id;
                    worksheet.Cell(currentRow, 2).Value = item.CreatedAt;
                    worksheet.Cell(currentRow, 3).Value = itemOrder;
                    worksheet.Cell(currentRow, 4).Value = item.TotalPrice;
                    worksheet.Cell(currentRow, 5).Value = item.OrderStatus;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orders.xlsx");
                }
            }
        }        
    }
}
