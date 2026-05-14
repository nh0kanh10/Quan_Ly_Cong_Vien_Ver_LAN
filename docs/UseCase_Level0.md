# Sơ đồ Use Case Tổng thể (Level 0) - Khu du lịch Đại Nam

Dưới đây là sơ đồ Use Case mức cao nhất (Level 0) thể hiện mối quan hệ giữa các Tác nhân (Actors) và các Phân hệ chức năng chính.

```mermaid
graph LR
    %% Định nghĩa các Actor
    Admin((Quản lý / Admin))
    HR((HR Nhân sự))
    KT((Kế toán))
    TN((Thu ngân))
    LT((Lễ tân))
    Kho((Nhân viên Kho))
    KT_VH((Kỹ thuật / Vận hành))
    KH((Khách hàng))

    subgraph "HỆ THỐNG KDL ĐẠI NAM (LEVEL 0)"
        direction TB
        UC1([UC01: Quản lý Danh mục & Hệ thống])
        UC2([UC02: Quản lý Bán hàng POS & Khuyến mãi])
        UC3([UC03: Quản lý Lưu trú & Khách sạn])
        UC4([UC04: Kiểm soát Vé & Cổng truy cập])
        UC5([UC05: Quản lý Kho bãi & Chuỗi cung ứng])
        UC6([UC06: Quản lý Tài chính & Ví RFID])
        UC7([UC07: Quản lý Nhân sự & Tiền lương])
        UC8([UC08: Quản lý Vận hành & Bảo trì])
    end

    %% Mối quan hệ Admin
    Admin --- UC1
    Admin --- UC5
    Admin --- UC7
    Admin --- UC8

    %% Mối quan hệ Thu ngân
    TN --- UC2
    TN --- UC6
    TN --- UC4

    %% Mối quan hệ Lễ tân
    LT --- UC3

    %% Mối quan hệ Kho
    Kho --- UC5

    %% Mối quan hệ Kế toán
    KT --- UC6

    %% Mối quan hệ HR
    HR --- UC7

    %% Mối quan hệ Kỹ thuật
    KT_VH --- UC4
    KT_VH --- UC8

    %% Mối quan hệ Khách hàng
    KH --- UC3
    KH --- UC8

    %% Styling
    style Admin fill:#f9f,stroke:#333,stroke-width:2px
    style KH fill:#bbf,stroke:#333,stroke-width:2px
    classDef usecase fill:#fff,stroke:#333,stroke-width:1px
    class UC1,UC2,UC3,UC4,UC5,UC6,UC7,UC8 usecase
```

### Giải thích các mối quan hệ chính:

1.  **Quản lý / Admin**: Có quyền can thiệp vào hầu hết các thiết lập hệ thống, phê duyệt các phiếu kho và nhân sự cấp cao.
2.  **Nhân viên vận hành (Thu ngân, Lễ tân, Kho)**: Tập trung vào các nghiệp vụ giao dịch và quản lý tài sản trực tiếp.
3.  **Kỹ thuật / Vận hành**: Đóng vai trò kép vừa kiểm soát cổng (soát vé) vừa thực hiện các tác vụ bảo trì hệ thống.
4.  **Khách hàng**: Tương tác với hệ thống qua các kênh đặt phòng (Web/App) và gửi phản hồi dịch vụ.

---
*Ghi chú: Đây là sơ đồ mức 0 nhằm mục đích giới thiệu kiến trúc chức năng. Các chi tiết logic cụ thể sẽ được trình bày ở các sơ đồ Level 1 và Sequence Diagram.*
