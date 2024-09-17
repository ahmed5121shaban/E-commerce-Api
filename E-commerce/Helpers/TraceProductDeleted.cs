using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text;

namespace E_commerce
{
    public class TraceProductDeleted:ActionFilterAttribute
    {
        string path = Directory.GetCurrentDirectory()
          + "/Logging/" + DateTime.Today.ToString("yy-MM-dd") + ".txt";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"On : {DateTime.Now.ToString()}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append($"UserID : {context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)} ");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append($"User Name : {context.HttpContext.User.FindFirstValue(ClaimTypes.Name)} ");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("The Action Is Product Deleting");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("--------------------------------------------------------------------------");
            stringBuilder.Append(Environment.NewLine);
            File.AppendAllText(path, stringBuilder.ToString());
        }
    }
}
