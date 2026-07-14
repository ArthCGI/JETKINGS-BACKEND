
﻿# JETKINGS-BACKEND
# JetKings API Documentation

## Base URL

```http
/api
```
  
---
 
# Buyer Product Prices

## Get All Buyer Product Prices
 
```http
GET /api/BuyerProductPrices?page=1&pageSize=10
```
 
### Query Parameters

- page (optional, default: 1)
- pageSize (optional, default: 10)

---

## Get Buyer Product Price By Id

```http
GET /api/BuyerProductPrices/{id}
```

Example:

```http
GET /api/BuyerProductPrices/1
```

---

## Create Buyer Product Price

```http
POST /api/BuyerProductPrices
```

Sample Request:

```json
{
  "buyerId": 1,
  "productId": 5,
  "price": 850
}
```

---

## Update Buyer Product Price

```http
PUT /api/BuyerProductPrices/{id}
```

Example:

```http
PUT /api/BuyerProductPrices/1
```

---

## Delete Buyer Product Price

```http
DELETE /api/BuyerProductPrices/{id}
```

---

# Buyers

## Get All Buyers

```http
GET /api/Buyers?page=1&pageSize=10
```

### Query Parameters

- page (optional, default: 1)
- pageSize (optional, default: 10)

---

## Get Buyer By Id

```http
GET /api/Buyers/{id}
```

Example:

```http
GET /api/Buyers/1
```

---

## Create Buyer

```http
POST /api/Buyers
```

Sample Request:

```json
{
  "buyerName": "ABC Traders",
  "contactPerson": "John",
  "phone": "9999999999"
}
```

---

## Update Buyer

```http
PUT /api/Buyers/{id}
```

---

## Delete Buyer

```http
DELETE /api/Buyers/{id}
```

---

# Dashboard

## Dashboard Summary

```http
GET /api/Dashboard/summary
```

Description:
Returns overall dashboard metrics and summary information.

---

## Recent Activities

```http
GET /api/Dashboard/recent-activities
```

Description:
Returns recent system/business activities.

---

## Buyers Summary

```http
GET /api/Dashboard/buyers-summary
```

Description:
Returns buyer-related dashboard statistics.

---

# Generate Bill

## Get Buyers (Dropdown)

```http
GET /api/GenerateBill/buyers
```

Description:
Returns all buyers for the buyer selection dropdown.

---

## Get Categories

```http
GET /api/GenerateBill/categories
```

Description:
Returns all product categories.

Sample Response:

```json
[
  {
    "id": 1,
    "name": "Mixers"
  },
  {
    "id": 2,
    "name": "Showers"
  }
]
```

---

## Get Products For Buyer

```http
GET /api/GenerateBill/products?buyerId=1
```

Description:
Returns all products available for the selected buyer including buyer-specific pricing.

Required Query Parameter:

- buyerId

Example:

```http
GET /api/GenerateBill/products?buyerId=1
```

---

## Get Products For Buyer By Category

```http
GET /api/GenerateBill/products?buyerId=1&categoryId=2
```

Description:
Returns products filtered by category and buyer-specific pricing.

Required Query Parameters:

- buyerId

Optional Query Parameters:

- categoryId

Example:

```http
GET /api/GenerateBill/products?buyerId=1&categoryId=2
```

---

# Products

## Get All Products

```http
GET /api/Products?page=1&pageSize=10
```

### Query Parameters

- page (optional, default: 1)
- pageSize (optional, default: 10)

---

## Get Product By Id

```http
GET /api/Products/{id}
```

Example:

```http
GET /api/Products/5
```

---

## Create Product

```http
POST /api/Products
```

Sample Request:

```json
{
  "categoryId": 2,
  "modelName": "Rain Shower 8 Inch",
  "defaultPrice": 1200
}
```

---

## Update Product

```http
PUT /api/Products/{id}
```

Example:

```http
PUT /api/Products/5
```

---

## Delete Product

```http
DELETE /api/Products/{id}
```

Example:

```http
DELETE /api/Products/5
```

---

## Get Products By Category

```http
GET /api/Products/category/{categoryId}
```

Example:

```http
GET /api/Products/category/2
```

Sample Response:

```json
{
  "success": true,
  "data": [
    {
      "id": 5,
      "categoryId": 2,
      "modelName": "Rain Shower 6 Inch",
      "defaultPrice": 900
    },
    {
      "id": 6,
      "categoryId": 2,
      "modelName": "Rain Shower 8 Inch",
      "defaultPrice": 1200
    },
    {
      "id": 7,
      "categoryId": 2,
      "modelName": "Hand Shower",
      "defaultPrice": 750
    }
  ]
}
```

---

# API Summary

```text
GET     /api/BuyerProductPrices
GET     /api/BuyerProductPrices/{id}
POST    /api/BuyerProductPrices
PUT     /api/BuyerProductPrices/{id}
DELETE  /api/BuyerProductPrices/{id}

GET     /api/Buyers
GET     /api/Buyers/{id}
POST    /api/Buyers
PUT     /api/Buyers/{id}
DELETE  /api/Buyers/{id}

GET     /api/Dashboard/summary
GET     /api/Dashboard/recent-activities
GET     /api/Dashboard/buyers-summary

GET     /api/GenerateBill/buyers
GET     /api/GenerateBill/categories
GET     /api/GenerateBill/products?buyerId={buyerId}
GET     /api/GenerateBill/products?buyerId={buyerId}&categoryId={categoryId}

GET     /api/Products
GET     /api/Products/{id}
POST    /api/Products
PUT     /api/Products/{id}
DELETE  /api/Products/{id}
GET     /api/Products/category/{categoryId}
```

