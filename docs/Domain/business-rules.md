# CommerceHub Business Rules

## Global Rules

- Every business entity belongs to a Tenant.
- Data isolation between tenants is mandatory.
- Soft delete is preferred over hard delete.
- All timestamps are stored in UTC.
- Domain events are raised for significant business actions.

---

# Product Rules

- Product name is required.
- Product name maximum length is 200 characters.
- SKU must be unique within a tenant.
- Product price cannot be negative.
- Product must belong to a category.
- Product may belong to one brand.
- Product must have exactly one primary image.
- Inactive products cannot be purchased.
- Deleted products cannot appear in search.

---

# Category Rules

- Category name is required.
- Category names must be unique within the same parent.
- Circular parent-child relationships are not allowed.
- Deleting a category with active products is prohibited.

---

# Brand Rules

- Brand name is required.
- Brand names must be unique per tenant.

---

# Inventory Rules

- Quantity cannot be negative.
- Reserved quantity cannot exceed available quantity.
- Stock reservation occurs before payment.
- Cancelling an order releases reserved inventory.
- Inventory adjustments require an audit trail.

---

# Customer Rules

- Email must be unique within a tenant.
- Email format must be valid.
- Customer may have multiple addresses.
- One default shipping address is allowed.
- One default billing address is allowed.

---

# Order Rules

- Order must contain at least one item.
- Order total is calculated from order items.
- Product prices are copied into OrderItems during checkout.
- Order cannot be modified after shipment.
- Shipped orders cannot be cancelled.
- Cancelled orders cannot be paid.
- Returned orders require a delivered order.

---

# Payment Rules

- Payment amount must equal order total.
- Payment cannot be captured twice.
- Refund amount cannot exceed paid amount.
- Failed payments may be retried.

---

# Shipment Rules

- Shipment requires a paid order.
- Shipment must contain at least one order item.
- Delivered shipments cannot be modified.
- Tracking number must be unique.

---

# Review Rules

- Only verified buyers can submit reviews.
- One review per customer per product.
- Rating must be between 1 and 5.
- Reviews cannot be edited after 30 days.

---

# Security Rules

- Users access only their tenant's data.
- Authorization is permission-based.
- JWT access tokens are short-lived.
- Refresh tokens are rotated after use.

---

# Auditing Rules

The following fields exist on all auditable entities:

- CreatedAt
- CreatedBy
- UpdatedAt
- UpdatedBy

---

# Concurrency Rules

- Aggregates use optimistic concurrency.
- Conflicting updates must fail gracefully.

---

# Domain Event Rules

The system publishes events for:

- ProductCreated
- ProductUpdated
- OrderPlaced
- OrderCancelled
- PaymentSucceeded
- PaymentFailed
- InventoryReserved
- InventoryReleased

Consumers must be idempotent.

---

# Non-Functional Rules

- API response time < 300 ms for standard operations.
- All write operations are transactional.
- Background processing is asynchronous.
- Every request is logged.
- Health checks expose service status.