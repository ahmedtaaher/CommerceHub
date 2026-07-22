# CommerceHub Entity Relationship Diagram

## Tenant

| Field | Type |
|---------|------|
| Id | Guid |
| Name | string |
| Slug | string |
| Status | enum |

Relationships

Tenant

- 1 → * Users
- 1 → * Products
- 1 → * Categories
- 1 → * Orders
- 1 → * Warehouses

---

# User

| Field | Type |
|---------|------|
| Id | Guid |
| TenantId | Guid |
| Name | string |
| Email | string |

Relationship

User

- * ↔ * Roles

---

# Category

| Field | Type |
|---------|------|
| Id | Guid |
| TenantId | Guid |
| Name | string |
| ParentCategoryId | Guid? |

Relationship

Category

- 1 → * Products
- 1 → * Child Categories

---

# Brand

| Field | Type |
|---------|------|
| Id | Guid |
| TenantId | Guid |
| Name | string |

Relationship

Brand

- 1 → * Products

---

# Product

| Field | Type |
|---------|------|
| Id | Guid |
| TenantId | Guid |
| CategoryId | Guid |
| BrandId | Guid |
| SKU | string |
| Name | string |
| Price | Money |
| Status | enum |

Relationship

Product

- 1 → * ProductImages
- 1 → * InventoryItems
- 1 → * OrderItems
- 1 → * Reviews

---

# ProductImage

| Field | Type |
|---------|------|
| Id | Guid |
| ProductId | Guid |
| Url | string |
| IsPrimary | bool |

---

# Warehouse

| Field | Type |
|---------|------|
| Id | Guid |
| TenantId | Guid |
| Name | string |

Relationship

Warehouse

- 1 → * InventoryItems

---

# InventoryItem

| Field | Type |
|---------|------|
| ProductId | Guid |
| WarehouseId | Guid |
| Quantity | int |
| ReservedQuantity | int |

Composite Key

(ProductId, WarehouseId)

---

# Customer

| Field | Type |
|---------|------|
| Id | Guid |
| TenantId | Guid |
| FirstName | string |
| LastName | string |
| Email | Email |

Relationship

Customer

- 1 → * Addresses
- 1 → * Orders
- 1 → * Reviews

---

# Address

Value Object

Street

City

State

Country

PostalCode

---

# Order

| Field | Type |
|---------|------|
| Id | Guid |
| TenantId | Guid |
| CustomerId | Guid |
| OrderNumber | string |
| Status | enum |
| Total | Money |

Relationship

Order

- 1 → * OrderItems
- 1 → 1 Payment
- 1 → * Shipments

---

# OrderItem

| Field | Type |
|---------|------|
| ProductId | Guid |
| ProductName | string |
| UnitPrice | Money |
| Quantity | int |

---

# Payment

| Field | Type |
|---------|------|
| Id | Guid |
| OrderId | Guid |
| Amount | Money |
| Status | enum |

---

# Shipment

| Field | Type |
|---------|------|
| Id | Guid |
| OrderId | Guid |
| Carrier | string |
| TrackingNumber | string |

---

# Review

| Field | Type |
|---------|------|
| Id | Guid |
| ProductId | Guid |
| CustomerId | Guid |
| Rating | int |
| Comment | string |

---

# Important Database Indexes

Product

- (TenantId, SKU) UNIQUE
- (TenantId, Name)

Category

- (TenantId, ParentCategoryId)

Inventory

- (WarehouseId, ProductId) UNIQUE

Order

- (TenantId, CustomerId)

Review

- (ProductId, CustomerId) UNIQUE

Payment

- (OrderId)