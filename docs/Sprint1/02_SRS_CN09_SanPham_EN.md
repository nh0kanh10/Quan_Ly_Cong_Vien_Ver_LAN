# Dai Nam Resort
# Software Requirements Specification
# Project Code: DN01
# Document Code: DN01_SRS_ProductManagement_v1.0

Ho Chi Minh City, April 2026

---

## Change Log

| Effective Date | Changed Item | A/M/D | Description | Version |
|---|---|---|---|---|
| 19/04/2026 | First release | A | | 1.0 |

*A - Add, M - Modify, D - Delete*

---

## Table of Contents

1. [Product & Service Management](#1-product--service-management)
   - 1.1. [Product List Screen](#11-product-list-screen)
   - 1.2. [Product Detail Screen — General Info Tab](#12-product-detail-screen--general-info-tab)
   - 1.3. [Pricing Tab](#13-pricing-tab)
   - 1.4. [Unit Conversion Tab](#14-unit-conversion-tab)
   - 1.5. [Operation Config Tab — Ticket Type](#15-operation-config-tab--ticket-type)
   - 1.6. [Operation Config Tab — F&B Type](#16-operation-config-tab--fb-type)
   - 1.7. [Delete Product](#17-delete-product)
2. [Other Requirements](#2-other-requirements)
   - 2.1. [Data Formats](#21-data-formats)
   - 2.2. [Reference Data Lists](#22-reference-data-lists)
   - 2.3. [Error Message Codes](#23-error-message-codes)

---

# 1. Product & Service Management

The product management module has these features:

- Search and filter product list
- Add new product
- Edit product info
- Set up pricing (multiple price levels, multiple time ranges)
- Set up unit conversion table
- Configure operations by type (turnstile ticket, F&B with BOM recipe)
- Soft-delete product

---

## 1.1. Product List Screen

### 1.1.1. Overview

This screen shows all products in a grid view. The left side is the product grid, the right side is a detail panel (Split View). When the user clicks a row on the grid, the right panel auto-loads that product's detail info.

### 1.1.2. Actors

- Manager (add, edit, delete products)
- Accountant (view, set up unit conversion, set up BOM)

### 1.1.3. Use-case Diagram

```
Manager ──── Search product
         ├── Add new product
         ├── Edit product
         ├── Set up pricing               <<extend>> Add/Edit
         ├── Set up unit conversion        <<extend>> Add/Edit
         ├── Configure turnstile access    <<extend>> Add/Edit (only when product is Ticket)
         ├── Configure BOM recipe          <<extend>> Add/Edit (only when product is F&B)
         └── Soft-delete product
```

#### 1.1.3.1. Pre-conditions

- User is logged in.
- Reference data for units, turnstiles, POS zones, and tax settings already exist.

#### 1.1.3.2. Post-conditions

Product data is saved to the database (tables: SanPham, BangGia, QuyDoiDonVi, and related type-specific tables).

#### 1.1.3.3. Trigger

User opens the Catalog menu, then picks the Goods & Services option.

### 1.1.4. User Flow

#### 1.1.4.1. Scenario 1 — Add a new service ticket

| | User | System |
|---|---|---|
| 1 | Clicks the Add New button. | Opens the right detail panel. General Info tab opens by default. All fields are empty. |
| 2 | Enters product code, ticket name. Picks unit as "Turn". Picks product type as Service Ticket. | Auto-generates code prefix: VE_. Tab 4 switches to Ticket Config. The "Is material" checkbox auto-unchecks and is grayed out. |
| 3 | Switches to Ticket Config tab, sets target: Adult. Adds a row in turnstile grid: Water Park Zone (1 turn). | Recorded. |
| 4 | Switches to Pricing tab, adds a price row: Default type, price 150,000. | Checks that date ranges don't overlap. |
| 5 | Clicks the Save button. | System checks all 4 tabs. If valid, saves everything, shows success message, and refreshes the product grid. |

#### 1.1.4.2. Scenario 2 — Add an F&B item

| | User | System |
|---|---|---|
| 1 | Clicks Add New. Enters name Heineken Beer, picks base unit as Can, product type as Beverage. Checks "Is material", picks VAT 8%. | Auto-generates prefix DU_. Tab 4 switches to F&B Config. |
| 2 | Switches to Unit Conversion tab, clicks Add Row, picks target unit as Case, ratio = 24. | Checks that ratio is greater than 0. |
| 3 | Checks the "Apply to all POS" box. | Grays out POS list, defaults to apply all. |
| 4 | Clicks Save. | Saves all tabs, shows success message. |

#### 1.1.4.3. Scenario 3 — Edit a product

| | User | System |
|---|---|---|
| 1 | Clicks a row on the product grid. | Right panel auto-loads that product's info. Product code and product type are locked (read-only). |
| 2 | Edits the fields that need changing (name, price, conversion...). | Marks the dirty flag. |
| 3 | Clicks Save. | System validates, saves, shows success, then refreshes the grid. |

#### 1.1.4.4. Scenario 4 — Unsaved changes warning when switching rows

| | User | System |
|---|---|---|
| 1 | While editing Product A (not saved yet), clicks on Product B in the grid. | Shows a confirm dialog with 3 buttons: Yes, No, Cancel. |
| 2a | Clicks Yes. | Saves Product A. If valid, switches to Product B. If error, stays on Product A. |
| 2b | Clicks No. | Discards changes, switches to Product B. |
| 2c | Clicks Cancel. | Stays on Product A, does not switch. |

### 1.1.5. UI Layout

#### 1.1.5.1. Screen Description — Toolbar

| # | Field Name | Control Type | Required | Data Type | Default Value | Description / Tooltip |
|---|---|---|---|---|---|---|
| 1 | Add New | Button | N/A | N/A | N/A | Opens detail panel in add mode. |
| 2 | Refresh | Button | N/A | N/A | N/A | Resets filter to All, reloads data. |
| 3 | Search | Text Field | No | Text | Blank | Live-filter on grid by product code or name. (*) Tooltip: "Type code or name to search" |

#### 1.1.5.2. Screen Description — Quick Filter

| # | Field Name | Control Type | Required | Data Type | Default Value | Description |
|---|---|---|---|---|---|---|
| 1 | All | Button (Toggle) | N/A | N/A | Selected | Show all product types |
| 2 | Ticket | Button (Toggle) | N/A | N/A | N/A | Filter for Zone Entry Tickets |
| 3 | Food | Button (Toggle) | N/A | N/A | N/A | Filter for Food items |
| 4 | Drink | Button (Toggle) | N/A | N/A | N/A | Filter for Drink items |
| 5 | Rental | Button (Toggle) | N/A | N/A | N/A | Filter for Locker / Rental items |
| 6 | Lodging | Button (Toggle) | N/A | N/A | N/A | Filter for Lodging items |
| 7 | Material | Button (Toggle) | N/A | N/A | N/A | Filter for Raw Material items |

The selected button is highlighted (primary color), other buttons show as dimmed.

#### 1.1.5.3. Screen Description — Product Grid (Grid Control, Read-only)

| # | Column Name | Control Type | Data Type | Description |
|---|---|---|---|---|
| 1 | Product Code | Label | Text | Product ID code |
| 2 | Product Name | Label | Text | Display name |
| 3 | Product Type | Label | Text | Product category group. This column is grouped so the grid shows a tree layout. Display value is translated for multi-language. |
| 4 | Status | Label | Text | Active, Paused, or Stopped. Display value is translated for multi-language. |
| 5 | Action | Button (Delete) | N/A | Delete button pinned on the right. Clicking shows a confirm dialog before soft-deleting. |

#### 1.1.5.4. Row Style — Product Grid

| Status | Text Color |
|---|---|
| DangBan | Green (Success) |
| TamNgung | Dark Yellow (Amber) |
| NgungBan | Red (Danger) |

#### 1.1.5.5. Screen Description — Status Bar

| # | Field Name | Control Type | Data Type | Description |
|---|---|---|---|---|
| 1 | Total Products | Label | Text | Shows: "Total {N}" where N = number of products on the grid |

### 1.1.6. Business Rules

| # | Name | Rule |
|---|---|---|
| 1 | Split View | Grid on the left, detail panel on the right. Detail panel is hidden until user clicks a row or presses Add New. |
| 2 | Group by type | Product Type column is grouped (GroupIndex = 0), grid shows a tree view. All groups expanded by default. |
| 3 | Quick filter | Click a filter button, system reloads data by product type. Selected button is highlighted. |
| 4 | Search | Type in the search box, grid filters instantly (client-side filter on Product Code and Product Name columns). |
| 5 | Multi-language | When language changes: reload entire grid, labels, buttons, filters, and detail panel. |
| 6 | Dirty tracking | When switching rows while the detail panel has unsaved data, system shows a 3-button confirm dialog (Yes, No, Cancel). |

### 1.1.7. Related Use-cases

- Product detail screen
- Delete product

---

## 1.2. Product Detail Screen — General Info Tab

### 1.2.1. Overview

This panel is loaded into the right half of the Split View. It has 4 tabs: General Info, Pricing, Unit Conversion, and Operation Config. The Operation Config tab (tab 4) is a dynamic panel — it shows different content depending on the product type.

### 1.2.2. UI Layout

#### 1.2.2.1. Screen Description — General Info Tab

| # | Field Name | Control Type | Required | Data Type | Default Value | Description / Tooltip |
|---|---|---|---|---|---|---|
| 1 | Avatar Image | Picture Edit | No | Image | Blank | Click the image to open a file picker (jpg, png, gif, webp). (*) Tooltip: "Click to pick an avatar" |
| 2 | Product Code | Text Field | Yes | Text | Blank (add mode) / Read-only (edit mode) | When adding: auto-generates prefix by product type (VE_, FB_, DU_...). User only types the suffix part. When editing: locked, cannot change. |
| 3 | Product Name | Text Field | Yes | Text | Blank | Max 150 characters. |
| 4 | Product Type | Image Combo Box | Yes | Text | Blank | Decides what shows in Tab 4 (Operation Config). Locked when in edit mode. (*) Tooltip: none — the action is self-explanatory |
| 5 | Base Unit | Search Lookup Edit | Yes | Integer | Blank | List of active units. (*) Tooltip: "Pick the smallest unit (e.g., Can instead of Case)" |
| 6 | VAT Tax | Search Lookup Edit | Yes | Integer | Blank | Tax config list (Code, Name, %VAT). |
| 7 | Status | Image Combo Box | Yes | Text | DangBan | Active, Paused, or Stopped. |
| 8 | POS Points | Checked Combo Box Edit | No | Text (multi-select) | Blank | Pick which kiosks/POS are allowed to sell this product. (*) Tooltip: Pick the allowed counters |
| 9 | Apply to all POS | Check Box | No | Boolean | Unchecked | If checked, grays out the POS list and auto-applies to all. |
| 10 | Is Material | Check Box | No | Boolean | Unchecked | If product type is a virtual service (Ticket), this auto-unchecks and is grayed out. (*) Tooltip: Product needs stock in/out tracking |
| 11 | Track Batches (Expiry) | Check Box | No | Boolean | Unchecked | If product type is a virtual service, this auto-unchecks and is grayed out. (*) Tooltip: Turn on for the system to track expiry dates by batch |
| 12 | Reference Price | Text Field (Read-only) | — | Text | "Not set" | Always locked. Pulls the price from the first default pricing row. (*) Tooltip: "Current selling price (read-only). Edit in Pricing tab." |
| 13 | Save | Button | N/A | N/A | N/A | Saves all 4 tabs. Hotkey: Ctrl+S. |
| 14 | Cancel | Button | N/A | N/A | N/A | If editing: reloads original data. If adding: resets to blank. Hotkey: Esc. |

### 1.2.3. Business Rules

| # | Name | Rule |
|---|---|---|
| 1 | Immutable Product Type | Once saved, Product Type is locked forever. Cannot change type to prevent data mismatch (turnstile grid, BOM, rental pricing). |
| 2 | Auto code prefix | When adding: changing product type auto-updates the code prefix (VE_, FB_, DU_...). If the code box has an old prefix (under 4 characters, ending with underscore), it gets replaced with the new prefix. |
| 3 | Virtual product | If product type is Ticket, the "Is material" and "Track batches" checkboxes auto-uncheck and lock. No stock tracking needed for tickets. |
| 4 | Pricing Survival | If status is Active but the pricing table is empty or has no price >= 0 (except for Material type), system auto-forces status to Paused. |
| 5 | Avatar Image | Image file is copied to Uploads/SanPham/ folder with a UUID name. Relative path is saved to the database. |
| 6 | Dirty tracking | Any edit on any control (text, combo, grid) triggers the dirty flag. |
| 7 | Hotkey | Ctrl+S = Save. Esc = Cancel. Tab key flows across the entire form. |

### 1.2.4. Validation Rules

| # | Rule | Message Code |
|---|---|---|
| 1 | Product Code is required, cannot be just a prefix | ERR_REQUIRED_MASP |
| 2 | Product Name is required | ERR_REQUIRED_TENSP |
| 3 | Product Type is required | ERR_REQUIRED_LOAISP |
| 4 | Base Unit is required | ERR_REQUIRED_DVT |
| 5 | Product Code already exists | ERR_TRUNG_MASP |
| 6 | Product Code only has the prefix, no suffix typed yet | ERR_MASP_CHI_TIENTO |

### 1.2.5. Related Use-cases

- Product list screen
- Pricing tab
- Unit Conversion tab
- Operation Config tab

---

## 1.3. Pricing Tab

### 1.3.1. Overview

This tab manages flexible pricing over time. You can create multiple price levels at the same time (default price, holiday price, promo price). If the product type is Rental, the grid auto-shows extra columns for rental surcharges.

### 1.3.2. UI Layout

#### 1.3.2.1. Screen Description — Pricing Grid (Grid Control, Editable)

| # | Column Name | Control Type | Required | Data Type | Default Value | Description / Tooltip |
|---|---|---|---|---|---|---|
| 1 | Price Type | Image Combo Box (in-grid) | Yes | Text | MacDinh | Default, Holiday, or Promo. |
| 2 | Valid From | Date Edit (in-grid) | Yes | Date | Today | Start date. Format dd/MM/yyyy. |
| 3 | Valid To | Date Edit (in-grid) | Yes | Date | Today + 1 year | End date. Format dd/MM/yyyy. |
| 4 | Selling Price | Spin Edit (in-grid) | Yes | Decimal(15,0) | 0 | Price per base unit. Format N0. |
| 5 | Deposit | Spin Edit (in-grid) | Conditional | Decimal(15,0) | 0 | Only shows when product type is Rental. |
| 6 | First Block Minutes | Spin Edit (in-grid) | Conditional | Integer | 0 | Only shows when product type is Rental. Number of minutes in the first price block. |
| 7 | Next Minutes | Spin Edit (in-grid) | Conditional | Integer | 0 | Only shows when product type is Rental. Time interval for the next surcharge block. |
| 8 | Surcharge Price | Spin Edit (in-grid) | Conditional | Decimal(15,0) | 0 | Only shows when product type is Rental. Price for each next block. |
| 9 | Delete | Button (Delete icon) | N/A | N/A | N/A | Delete row button, pinned on the right. |

**Adding a row:** Click the "Add Row" button at the bottom of the grid. A new row is created with default values.

### 1.3.3. Business Rules

| # | Name | Rule |
|---|---|---|
| 1 | No time overlap | Cannot create 2 price levels of the same type (e.g., both Default) with overlapping date ranges. |
| 2 | Price type priority | Holiday > Promo > Default. Holiday prices can overlap any date without error. |
| 3 | Rental columns | Deposit, First Block Minutes, Next Minutes, Surcharge Price columns only show when product type is Rental (TuDo, DoCho, ChoiNghiMat). Other types hide these columns completely. |
| 4 | Highlight edited rows | Rows that were just edited (not saved yet) get a light yellow background (#FFFFE6). |
| 5 | Inline edit | Users edit directly on the grid, no popup needed. Values auto-save when leaving a cell. |

### 1.3.4. Related Use-cases

- General Info tab

---

## 1.4. Unit Conversion Tab

### 1.4.1. Overview

This tab solves the problem of buying in bulk packaging but selling in base units. Each row is a rule: "1 Target Unit = N Base Units".

### 1.4.2. UI Layout

#### 1.4.2.1. Screen Description — Conversion Grid (Grid Control, Editable)

| # | Column Name | Control Type | Required | Data Type | Default Value | Description / Tooltip |
|---|---|---|---|---|---|---|
| 1 | Target Unit | Search Lookup Edit (in-grid) | Yes | Integer | Blank | Pick a new unit (e.g., "Case", "Pack"). Only shows active units. |
| 2 | Ratio | Spin Edit (in-grid) | Yes | Decimal | 1 | Conversion rate: 1 Target Unit = N Base Units (e.g., 1 Case = 24 Cans). Format: 0.#### (auto-trims trailing zeros). |
| 3 | Delete | Button (Delete icon) | N/A | N/A | N/A | Delete row button, pinned on the right. |

**Adding a row:** Click the "Add Row" button at the bottom of the grid.

### 1.4.3. Business Rules

| # | Name | Rule |
|---|---|---|
| 1 | Atomic unit | Each product has only 1 code with the smallest base unit. Strictly no creating product variants by packaging size. |
| 2 | Positive ratio | Conversion ratio must be a positive number. If entered as zero, negative, or left blank, system shows an error. |
| 3 | Highlight edited rows | Rows that were just edited get a light yellow background (#FFFFE6). |
| 4 | Flexible pricing | If the selling price in the conversion config is left blank, the system auto-shows "Auto-calc" text, and selling will auto-calculate the price as base price × conversion ratio. If the user enters a specific amount here, the system uses that fixed price when selling, ignoring any base price changes. |

### 1.4.4. Validation Rules

| # | Rule | Message Code |
|---|---|---|
| 1 | Conversion ratio must be a valid positive number | ERR_HESO_KHONGHOPLE |

### 1.4.5. Related Use-cases

- General Info tab

---

## 1.5. Operation Config Tab — Ticket Type

### 1.5.1. Overview

This tab only shows when product type is Ticket (VeVaoKhu or VeTroChoi). It lets you configure the target group (adult / child) and the list of turnstile access rights.

### 1.5.2. Display Condition

Product type is VeVaoKhu or VeTroChoi.

### 1.5.3. UI Layout

#### 1.5.3.1. Screen Description

| # | Field Name | Control Type | Required | Data Type | Default Value | Description / Tooltip |
|---|---|---|---|---|---|---|
| 1 | Enable Access Rights | Check Box | No | Boolean | Unchecked | Turn on to allow ticket scanning at turnstiles. |
| 2 | Target Group | Combo Box | Yes | Text | Blank | "Adult" / "Child" / "All". |
| 3 | Access Rights Grid | Grid Control (Editable) | Conditional | — | Blank | Columns: Zone + Number of Turns. Adding a row = adding a zone where the ticket can be scanned. |

### 1.5.4. Business Rules

| # | Name | Rule |
|---|---|---|
| 1 | Single-ride ticket = 1 zone only | If it's a single-ride game ticket, system blocks adding a 2nd row in the access rights grid. |
| 2 | Cannot be empty | If "Enable Access Rights" is checked but the grid is empty when saving, system shows an error. |

### 1.5.5. Validation Rules

| # | Rule | Message Code |
|---|---|---|
| 1 | Turnstile access rights grid cannot be empty when enabled | ERR_LUOI_CONG_RONG |

### 1.5.6. Related Use-cases

- General Info tab

---

## 1.6. Operation Config Tab — F&B Type

### 1.6.1. Overview

This tab only shows when product type is F&B (AnUong or DoUong). It lets you configure allergy warnings, the kitchen/bar that prepares the dish, and the BOM recipe grid (list of ingredients with amounts).

### 1.6.2. Display Condition

Product type is AnUong or DoUong.

### 1.6.3. UI Layout

#### 1.6.3.1. Screen Description — F&B General Info

| # | Field Name | Control Type | Required | Data Type | Default Value | Description |
|---|---|---|---|---|---|---|
| 1 | Allergy Warning | Memo Edit | No | Text | Blank | Notes about allergy-causing ingredients. |
| 2 | Kitchen / Bar | Search Lookup Edit | No | Integer | Blank | Pick the kitchen or bar that makes this item. |

#### 1.6.3.2. Screen Description — BOM Grid (Grid Control, Editable)

| # | Column Name | Control Type | Required | Data Type | Default Value | Description / Tooltip |
|---|---|---|---|---|---|---|
| 1 | Ingredient | Search Lookup Edit (in-grid) | Yes | Integer | Blank | Pick a raw material. Blocks picking service products (Tickets) as ingredients. (*) Tooltip: "Type to search materials" |
| 2 | Unit | Label (Read-only) | — | Text | Blank | Auto-fills with the ingredient's base unit after picking. |
| 3 | Usage Amount | Spin Edit (in-grid) | Yes | Decimal(18,3) | 1.000 | Format N3. Allows micro amounts (e.g., 0.002 kg). No rounding. (*) Tooltip: "Amount used per 1 finished product" |
| 4 | Delete | Button (Delete icon) | N/A | N/A | N/A | Delete row button, pinned on the right. |

**Adding a row:** Click the "Add Row" button at the bottom of the grid.

### 1.6.4. Business Rules

| # | Name | Rule |
|---|---|---|
| 1 | Block virtual products | Ingredient lookup only lists products with the "Is material" flag turned on. Blocks picking Tickets or virtual services as ingredients. |
| 2 | BOM is cache only | All BOM rows on the grid only live in cache. Only when user clicks Save on the parent form, the data gets written to the database. |
| 3 | Highlight edited rows | Rows that were just edited get a light yellow background (#FFFFE6). |

### 1.6.5. Related Use-cases

- General Info tab

---

## 1.7. Delete Product

### 1.7.1. Overview

Soft-delete feature for products. The system does not physically delete — it just sets the flag DaXoa = 1.

### 1.7.2. Actor

Manager.

### 1.7.3. User Flow

| | User | System |
|---|---|---|
| 1 | Clicks the Delete button in the action column on the grid. | Shows a delete confirm dialog. |
| 2 | Clicks Yes. | Checks rules: stock > 0 or has pending orders. If yes, refuses and shows the reason. If no, sets the DaXoa flag and refreshes the grid. |

### 1.7.4. Business Rules

| # | Name | Rule |
|---|---|---|
| 1 | Check stock | If stock quantity > 0, system refuses to delete and shows a message. |
| 2 | Check orders | If there are unpaid orders linked to this product, system refuses to delete and shows a message. |
| 3 | Soft delete data | The system only marks the product as deleted instead of actually removing the data. This protects past invoices and accounting records from losing their linked info. Once deleted, the product no longer shows on any sales or management screens. |

### 1.7.5. Validation Rules

| # | Rule | Message Code |
|---|---|---|
| 1 | Product still has stock | ERR_CONTONKHO |
| 2 | Product is in a pending order | ERR_CONDONHANG |

### 1.7.6. Related Use-cases

- Product list screen

---

# 2. Other Requirements

## 2.1. Data Formats

### 2.1.1. Date & Time

- Default date format: dd/MM/yyyy. Example: 19/04/2026

### 2.1.2. Numbers

- Money: comma as thousand separator, no decimal places. Example: 150,000
- Conversion ratio: format 0.#### (auto-trims trailing zeros). Example: 24, 4.5
- BOM amount: format N3. Example: 0.002

## 2.2. Reference Data Lists

### 2.2.1. Product Types

| Code | Display Name | Code Prefix |
|---|---|---|
| VeVaoKhu | Zone Entry Ticket | VE_ |
| VeTroChoi | Game Ticket | VE_ |
| AnUong | Food | FB_ |
| DoUong | Beverage | DU_ |
| HangHoa | Goods | HH_ |
| TuDo | Locker / Rental | CT_ |
| DoCho | Animal Feed (Pet) | DC_ |
| ChoiNghiMat | Rest Shelter | CN_ |
| LuuTru | Lodging | LT_ |
| NguyenLieu | Raw Material | NL_ |
| GuiXe | Parking | GX_ |
| DatChoThuAn | Animal Feeding Spot | DA_ |

### 2.2.2. Product Status

| Code | Display Name |
|---|---|
| DangBan | Active |
| TamNgung | Paused |
| NgungBan | Stopped |

### 2.2.3. Price Types

| Code | Display Name |
|---|---|
| MacDinh | Default |
| NgayLe | Holiday |
| KhuyenMai | Promo |

## 2.3. Error Message Codes

| Message Code | English Content |
|---|---|
| ERR_REQUIRED_MASP | Product code is required |
| ERR_REQUIRED_TENSP | Product name is required |
| ERR_REQUIRED_LOAISP | Product type is required |
| ERR_REQUIRED_DVT | Base unit is required |
| ERR_TRUNG_MASP | Product code already exists: {0} |
| ERR_MASP_CHI_TIENTO | Product code cannot be just a prefix. Please type more or scan a barcode. |
| ERR_HESO_KHONGHOPLE | Conversion ratio must be a valid positive number |
| ERR_LUOI_CONG_RONG | Turnstile access rights grid cannot be empty |
| ERR_CONTONKHO | Product still has stock, cannot delete |
| ERR_CONDONHANG | Product is in a pending order, cannot delete |
| MSG_LUU_THANH_CONG | Saved successfully |
| MSG_LUU_THAT_BAI | Save failed |
| MSG_UNSAVED | You have unsaved changes! Do you want to save before switching? |
| MSG_XOA_THANH_CONG | Product deleted |
