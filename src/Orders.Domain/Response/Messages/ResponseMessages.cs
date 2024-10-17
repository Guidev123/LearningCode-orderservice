using System.ComponentModel;
using System.Reflection;

namespace Orders.Domain.Response.Messages;

public enum ResponseMessages
{
    [Description("Success: Order has been canceled successfully.")]
    ORDER_CANCELED_SUCCESS,
    [Description("Error: Unable to cancel order.")]
    ORDER_CANCELLATION_FAILED,
    [Description("Error: Order not found.")]
    ORDER_NOT_FOUND,
    [Description("Success: Order has been created successfully.")]
    ORDER_CREATED_SUCCESS,
    [Description("Error: Unable to create order.")]
    ORDER_CREATION_FAILED,
    [Description("Error: Invalid order details.")]
    INVALID_ORDER_DETAILS,
    [Description("Success: Orders retrieved successfully.")]
    ORDERS_RETRIEVED_SUCCESS,
    [Description("Error: Unable to retrieve orders.")]
    ORDERS_RETRIEVAL_FAILED,
    [Description("Success: Order retrieved successfully.")]
    ORDER_RETRIEVED_SUCCESS,
    [Description("Error: Unable to retrieve order.")]
    ORDER_RETRIEVAL_FAILED,
    [Description("Success: Payment processed successfully.")]
    PAYMENT_SUCCESS,
    [Description("Error: Unable to process payment.")]
    PAYMENT_FAILED,
    [Description("Error: Payment declined.")]
    PAYMENT_DECLINED,
    [Description("Success: Order refunded successfully.")]
    REFUND_SUCCESS,
    [Description("Error: Unable to process refund.")]
    REFUND_FAILED,
    [Description("Error: Order not eligible for refund.")]
    REFUND_NOT_ELIGIBLE,
    [Description("Success: Products retrieved successfully.")]
    PRODUCTS_RETRIEVED_SUCCESS,
    [Description("Error: Unable to retrieve products.")]
    PRODUCTS_RETRIEVAL_FAILED,
    [Description("Success: Product retrieved successfully.")]
    PRODUCT_RETRIEVED_SUCCESS,
    [Description("Error: Unable to retrieve product.")]
    PRODUCT_RETRIEVAL_FAILED,
    [Description("Error: Product not found.")]
    PRODUCT_NOT_FOUND,
    [Description("Success: Voucher retrieved successfully.")]
    VOUCHER_RETRIEVED_SUCCESS,
    [Description("Error: Unable to retrieve voucher.")]
    VOUCHER_RETRIEVAL_FAILED,
    [Description("Error: Voucher not found.")]
    VOUCHER_NOT_FOUND,
    [Description("Error: The voucher is inactive.")]
    VOUCHER_INACTIVE,
    [Description("Alert: This order has already been refunded.")]
    ORDER_ALREADY_REFUNDED,
    [Description("Alert: This order has already been paid.")]
    ORDER_ALREADY_PAID,
    [Description("Error: This order cannot be canceled.")]
    ORDER_CANNOT_BE_CANCELED,
    [Description("Alert: This order has already been canceled.")]
    ORDER_ALREADY_CANCELED,
    [Description("Error: The order cannot be paid.")]
    ORDER_CANNOT_BE_PAID,
    [Description("Success: The order has been paid successfully.")]
    ORDER_PAID_SUCCESS,
    [Description("Error: The order has not been paid yet, so it cannot be refunded.")]
    ORDER_NOT_PAID_CANNOT_BE_REFUNDED,
    [Description("Error: The order has already been canceled, so it cannot be refunded.")]
    ORDER_ALREADY_CANCELED_CANNOT_BE_REFUNDED,
    [Description("Error: The order cannot be refunded.")]
    ORDER_CANNOT_BE_REFUNDED,
    [Description("Success: The order has been refunded successfully.")]
    ORDER_REFUNDED_SUCCESS

}
public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            if (attribute != null)
                return attribute.Description;
        }

        return value.ToString();
    }
}
