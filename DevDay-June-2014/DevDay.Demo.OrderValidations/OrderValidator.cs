namespace DevDay.Demo.OrderValidations
{
    public class OrderValidator
    {
        public bool Validate(string orderNumber, out string message)
        {
            if (orderNumber.ToUpper().Contains("INVALID"))
            {
                message = string.Format("Order '{0}' did't pass validation checks", orderNumber);
                return false;
            }
            
            message = string.Format("Order '{0}' is valid", orderNumber);
            return true;
        }
    }
}