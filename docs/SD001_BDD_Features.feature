# language: vi
# BDD Feature Files — Dự án SD001 Quản Lý KDL Đại Nam
# Tích hợp Jira: Mỗi Scenario gắn tag @SD-xxx tương ứng ticket
# Tích hợp Confluence: Xem ADR-005, ADR-006
# Framework: SpecFlow (.NET) / Gherkin syntax
# Tác giả: Nhóm NguyenTanNhi — Sprint 3-5

# ============================================================
# MODULE: BÁN HÀNG POS (frmBanHang v2)
# Jira Epic: Bán Hàng POS
# Tickets: SD-027 (POS v2), SD-012 (POS v1)
# ============================================================

Feature: Bán hàng POS
  Là nhân viên bán vé tại quầy POS cảm ứng 15.6 inch,
  tôi muốn bán vé và dịch vụ cho khách hàng nhanh chóng,
  để giảm thời gian xếp hàng và tăng doanh thu.

  Background:
    Given hệ thống có sản phẩm:
      | Mã SP   | Tên               | Giá bán   | Tồn kho |
      | SP001   | Vé người lớn      | 200,000đ  | 500     |
      | SP002   | Vé trẻ em         | 100,000đ  | 500     |
      | SP003   | Combo gia đình    | 450,000đ  | 100     |
      | SP004   | Áo phao           | 50,000đ   | 200     |
    And nhân viên "Nguyên" đã đăng nhập với quyền "Thu ngân"

  # --- HAPPY PATH ---

  @SD-027 @happy-path @priority-high
  Scenario: Bán vé thành công — thanh toán tiền mặt
    Given giỏ hàng trống
    When nhân viên thêm 2 "Vé người lớn" vào giỏ hàng
    And nhân viên thêm 1 "Vé trẻ em" vào giỏ hàng
    Then giỏ hàng hiển thị 3 sản phẩm
    And tổng tiền = 500,000đ
    When nhân viên nhấn "Thanh toán tiền mặt"
    Then đơn hàng được tạo với trạng thái "Đã thanh toán"
    And kho xuất 2 "Vé người lớn" và 1 "Vé trẻ em"
    And tồn kho "Vé người lớn" giảm còn 498
    And giỏ hàng được xóa sạch để phục vụ khách tiếp theo

  @SD-027 @happy-path @loyalty
  Scenario: Bán vé cho khách VIP — tự động tích điểm Loyalty
    Given khách hàng "Nguyễn Văn A" (KH00001) có hạng VIP, hệ số nhân điểm = 2
    And khách có 1,500 điểm tích lũy hiện tại
    When nhân viên chọn khách "Nguyễn Văn A"
    And nhân viên thêm 1 "Combo gia đình" vào giỏ hàng
    And nhân viên nhấn "Thanh toán tiền mặt"
    Then đơn hàng ghi nhận MaKH = "KH00001"
    And khách được tích thêm 900 điểm (450,000 × 2 hệ số ÷ 1,000)
    And tổng điểm mới = 2,400 điểm
    And tổng chi tiêu khách cập nhật thêm 450,000đ

  @SD-027 @happy-path @rfid
  Scenario: Thanh toán bằng ví RFID (F10)
    Given khách hàng "Trần Thị B" có ví RFID số dư 1,000,000đ
    When nhân viên thêm 2 "Vé người lớn" vào giỏ hàng
    And nhân viên chọn khách "Trần Thị B"
    And nhân viên nhấn phím F10 (Thanh toán ví RFID)
    Then ví bị trừ 400,000đ
    And số dư ví còn 600,000đ
    And phiếu thu được tạo với hình thức "Ví RFID"

  @SD-027 @happy-path @dynamic-pricing
  Scenario: Áp dụng giá cuối tuần tự động
    Given hôm nay là Thứ Bảy (cuối tuần)
    And bảng giá cuối tuần: "Vé người lớn" = 250,000đ (tăng 25%)
    When nhân viên thêm 1 "Vé người lớn" vào giỏ hàng
    Then giá hiển thị = 250,000đ (giá cuối tuần)
    And tổng tiền = 250,000đ

  # --- NEGATIVE / SECURITY ---

  @SD-027 @security @anti-hack
  Scenario: Chống gian lận giá — client gửi giá 0đ
    Given sản phẩm "Vé người lớn" có giá server = 200,000đ
    When client gửi yêu cầu thêm sản phẩm với giá = 0đ
    Then hệ thống bỏ qua giá client
    And tự tra cứu giá từ cơ sở dữ liệu = 200,000đ
    And đơn hàng ghi nhận đúng giá 200,000đ/vé

  @SD-027 @negative @inventory
  Scenario: Từ chối bán khi hết hàng tồn kho
    Given sản phẩm "Áo phao" tồn kho = 0
    When nhân viên thêm 1 "Áo phao" vào giỏ hàng
    Then hệ thống thông báo "Sản phẩm đã hết hàng"
    And giỏ hàng không thay đổi

  @SD-027 @negative @rfid
  Scenario: Thanh toán ví RFID — số dư không đủ
    Given khách hàng "Lê Văn C" có ví RFID số dư 50,000đ
    When nhân viên thêm 2 "Vé người lớn" (tổng 400,000đ)
    And nhân viên nhấn F10 (Thanh toán ví RFID)
    Then hệ thống thông báo "Số dư ví không đủ. Còn: 50,000đ. Cần: 400,000đ"
    And đơn hàng KHÔNG được tạo
    And ví KHÔNG bị trừ tiền

  @SD-027 @edge-case
  Scenario: Số lượng âm — hệ thống từ chối
    When nhân viên thêm -5 "Vé người lớn" vào giỏ hàng
    Then hệ thống thông báo "Số lượng phải lớn hơn 0"
    And giỏ hàng không thay đổi

  @SD-027 @edge-case @quick-cash
  Scenario: Quick Cash — thanh toán nhanh F1-F4
    Given giỏ hàng có 1 "Vé người lớn" (200,000đ)
    When nhân viên nhấn F1 (Quick Cash — đưa đúng tiền)
    Then đơn hàng tạo ngay lập tức, không cần xác nhận thêm
    And tiền thừa = 0đ

# ============================================================
# MODULE: KHÁCH SẠN (frmDatPhong)
# Jira Epic: Dịch Vụ Giải Trí
# Tickets: SD-028
# ============================================================

Feature: Đặt phòng khách sạn Đại Nam
  Là nhân viên lễ tân khách sạn Đại Nam,
  tôi muốn đặt phòng, check-in, check-out cho khách,
  để quản lý 200+ phòng hiệu quả và tối đa công suất.

  Background:
    Given hệ thống có danh sách phòng:
      | Mã phòng | Loại phòng | Giá/đêm    | Trạng thái |
      | P101     | Standard   | 500,000đ   | Trống      |
      | P102     | Standard   | 500,000đ   | Trống      |
      | P201     | Deluxe     | 800,000đ   | Đang ở     |
      | P301     | VIP Suite  | 1,500,000đ | Trống      |
    And nhân viên "Tấn" đã đăng nhập với quyền "Lễ tân"

  # --- HAPPY PATH ---

  @SD-028 @happy-path @booking
  Scenario: Đặt phòng và check-in thành công
    Given khách "Nguyễn Văn A" có ví RFID số dư 2,000,000đ
    When nhân viên chọn phòng P101 (Standard, 500,000đ/đêm)
    And nhập ngày check-in: 10/04/2026, ngày check-out: 12/04/2026 (2 đêm)
    And nhấn "Đặt phòng"
    Then đơn đặt phòng được tạo với:
      | Trường       | Giá trị          |
      | Tổng tiền    | 1,000,000đ       |
      | Tiền cọc 50% | 500,000đ         |
      | Trạng thái   | Đã đặt           |
    And ví RFID bị trừ 500,000đ (cọc 50%)
    And phòng P101 chuyển trạng thái "Đã đặt"

  @SD-028 @happy-path @checkout
  Scenario: Check-out và thanh toán phần còn lại
    Given khách "Nguyễn Văn A" đang ở phòng P101 từ 10/04 (đã cọc 500,000đ)
    When nhân viên nhấn "Trả phòng" cho P101 vào ngày 12/04
    Then hệ thống tính:
      | Hạng mục     | Giá trị          |
      | Tổng tiền    | 1,000,000đ       |
      | Đã cọc       | 500,000đ         |
      | Còn phải trả | 500,000đ         |
    And phòng P101 chuyển trạng thái "Trống"
    And phiếu thu được tạo ghi nhận 500,000đ còn lại

  @SD-028 @happy-path @group
  Scenario: Đặt phòng cho đoàn khách (10 phòng)
    Given đoàn "ABC Travel" gồm 20 khách
    When nhân viên mở dialog "Đặt phòng đoàn"
    And chọn 10 phòng Standard (P101-P110)
    And nhập ngày: 15/04 đến 17/04 (2 đêm)
    Then tổng tiền đoàn = 10,000,000đ (10 phòng × 500,000đ × 2 đêm)
    And cọc đoàn 50% = 5,000,000đ
    And 10 phòng chuyển trạng thái "Đã đặt"

  # --- NEGATIVE ---

  @SD-028 @negative @overbooking
  Scenario: Chống đặt phòng trùng lịch (overbooking)
    Given phòng P101 đã được đặt từ 10/04 đến 14/04
    When nhân viên thử đặt P101 cho khách khác từ 12/04 đến 15/04
    Then hệ thống thông báo "Phòng P101 đã có khách từ 10/04 đến 14/04"
    And đơn đặt phòng KHÔNG được tạo

  @SD-028 @negative @deposit
  Scenario: Check-in khi ví không đủ cọc
    Given khách "Lê Văn C" có ví RFID số dư 100,000đ
    When nhân viên đặt phòng P301 (VIP Suite, 1,500,000đ/đêm, cọc 50% = 750,000đ)
    Then hệ thống thông báo "Số dư ví không đủ cọc. Cần: 750,000đ. Có: 100,000đ"
    And phòng P301 vẫn "Trống"

  @SD-028 @edge-case @early-checkout
  Scenario: Trả phòng sớm — tính tiền thực tế
    Given khách đặt phòng P101 từ 10/04 đến 15/04 (5 đêm, cọc 1,250,000đ)
    When khách trả phòng sớm vào 12/04 (thực tế ở 2 đêm)
    Then tổng tiền tính theo thực tế = 1,000,000đ (2 đêm × 500,000đ)
    And hoàn lại khách 250,000đ (cọc 1,250,000 − thực tế 1,000,000)

# ============================================================
# MODULE: NHÀ HÀNG (frmNhaHang + frmDatBan)
# Jira Epic: Dịch Vụ Giải Trí
# Tickets: SD-029
# ============================================================

Feature: Quản lý nhà hàng Đại Nam
  Là nhân viên phục vụ nhà hàng,
  tôi muốn quản lý bàn, gọi món, ghép bàn, thanh toán,
  để phục vụ khách nhanh chóng trong giờ cao điểm.

  Background:
    Given hệ thống có danh sách bàn:
      | Mã bàn | Khu vực | Sức chứa | Trạng thái |
      | B01    | Tầng 1  | 4        | Trống      |
      | B02    | Tầng 1  | 4        | Trống      |
      | B03    | VIP     | 8        | Trống      |
      | B04    | Sân vườn| 6        | Đang phục vụ|
    And menu nhà hàng:
      | Mã món | Tên món         | Giá       |
      | M001   | Cơm gà nướng   | 75,000đ   |
      | M002   | Lẩu thái       | 250,000đ  |
      | M003   | Nước cam        | 25,000đ   |

  # --- HAPPY PATH ---

  @SD-029 @happy-path @order
  Scenario: Mở bàn → Gọi món → Thanh toán
    When nhân viên nhấn "Mở bàn" cho B01
    Then B01 chuyển trạng thái "Đang phục vụ"
    And đơn đặt bàn được tạo
    When nhân viên gọi 2 "Cơm gà nướng" và 1 "Lẩu thái" cho B01
    Then bill B01 hiển thị:
      | Món            | SL | Thành tiền |
      | Cơm gà nướng   | 2  | 150,000đ   |
      | Lẩu thái       | 1  | 250,000đ   |
      | **Tổng**       |    | **400,000đ**|
    When nhân viên nhấn "Thanh toán" cho B01, chọn "Tiền mặt"
    Then đơn hàng trạng thái "Đã thanh toán"
    And B01 chuyển về "Trống"

  @SD-029 @happy-path @merge
  Scenario: Ghép bàn (merge bill) — 2 bàn thành 1 hóa đơn
    Given B01 đang phục vụ, bill = 200,000đ
    And B02 đang phục vụ, bill = 150,000đ
    When nhân viên chọn "Ghép bàn" B01 + B02
    Then bill được gộp thành 1 đơn, tổng = 350,000đ
    And B02 chuyển "Trống" (đã gộp vào B01)
    And B01 vẫn "Đang phục vụ" với bill mới

  @SD-029 @happy-path @reservation
  Scenario: Đặt bàn trước + cọc ví RFID
    Given khách "Phạm Văn D" gọi đặt bàn VIP (B03) lúc 19:00 tối 12/04
    And khách có ví RFID số dư 500,000đ
    When nhân viên tạo đơn đặt bàn trước:
      | Trường       | Giá trị          |
      | Bàn          | B03 (VIP, 8 chỗ) |
      | Ngày giờ     | 12/04 19:00      |
      | Số khách     | 6                |
      | Tiền cọc     | 100,000đ         |
    Then ví bị trừ 100,000đ (cọc)
    And B03 chuyển trạng thái "Đã đặt trước"
    And hệ thống ghi nhận lịch đặt bàn

  # --- NEGATIVE ---

  @SD-029 @negative @double-booking
  Scenario: Chống đặt bàn đã có người
    Given B03 đã được đặt trước lúc 19:00 ngày 12/04
    When nhân viên thử đặt B03 cho khách khác cùng giờ
    Then hệ thống thông báo "Bàn B03 đã có đặt trước lúc 19:00"
    And đơn đặt KHÔNG được tạo

  @SD-029 @negative @empty-bill
  Scenario: Thanh toán bàn chưa gọi món
    Given B01 vừa mở bàn, chưa gọi món nào
    When nhân viên nhấn "Thanh toán" cho B01
    Then hệ thống thông báo "Bàn chưa có món. Vui lòng gọi món trước"

# ============================================================
# MODULE: KHÁCH HÀNG + VÍ RFID (frmKhachHang + frmViDienTu)
# Jira Epic: Nền Tảng & Danh Mục + Tài Chính & Ví
# Tickets: SD-011, SD-015, SD-034, SD-044
# ============================================================

Feature: Quản lý khách hàng Customer 360 + Ví RFID
  Là nhân viên chăm sóc khách hàng,
  tôi muốn quản lý thông tin, ví RFID, lịch sử, điểm tích lũy,
  để cung cấp trải nghiệm Customer 360 toàn diện.

  # --- HAPPY PATH ---

  @SD-011 @happy-path @create
  Scenario: Thêm khách hàng mới + tự động sinh mã
    When nhân viên nhập thông tin:
      | Trường       | Giá trị           |
      | Họ tên       | Trần Thị Mai      |
      | Số điện thoại| 0901234567        |
      | Email        | mai@email.com     |
    And nhấn "Thêm"
    Then hệ thống tự sinh mã "KH00042" (số tiếp theo)
    And thông báo "Thêm khách hàng thành công"
    And khách xuất hiện trong danh sách

  @SD-015 @happy-path @rfid
  Scenario: Cấp thẻ RFID + Nạp tiền ví
    Given khách "Trần Thị Mai" (KH00042) chưa có thẻ RFID
    When nhân viên quét thẻ RFID mới (mã: RFID-2026-0099)
    And gắn thẻ cho khách KH00042
    And nạp 500,000đ vào ví
    Then ví hiển thị số dư = 500,000đ
    And lịch sử giao dịch ghi nhận "Nạp tiền: +500,000đ"

  @SD-034 @happy-path @loyalty
  Scenario: Nâng hạng VIP khi đạt ngưỡng điểm
    Given khách "Nguyễn Văn A" hạng "Thường", điểm = 9,800
    When khách mua vé 300,000đ (tích thêm 300 điểm)
    Then tổng điểm = 10,100
    And hệ thống tự nâng hạng lên "VIP" (ngưỡng 10,000 điểm)
    And hệ số nhân điểm thay đổi từ 1 → 2

  # --- NEGATIVE ---

  @SD-011 @negative @duplicate
  Scenario: Chống trùng số điện thoại
    Given hệ thống đã có khách SĐT "0901234567"
    When nhân viên thêm khách mới với SĐT "0901234567"
    Then hệ thống thông báo "Số điện thoại đã được đăng ký bởi khách KH00042"
    And KHÔNG lưu khách mới

  @SD-011 @negative @validation
  Scenario: Số điện thoại không hợp lệ
    When nhân viên nhập SĐT "12345" (chỉ 5 ký tự)
    And nhấn "Thêm"
    Then hệ thống thông báo "SĐT phải gồm 10 chữ số, bắt đầu bằng 0"

  @SD-011 @negative @soft-delete
  Scenario: Xóa mềm khách — không xóa vĩnh viễn
    Given khách "Trần Thị Mai" có ví RFID số dư 200,000đ
    And khách có 3 đơn hàng trong lịch sử
    When nhân viên nhấn "Xóa" khách "Trần Thị Mai"
    Then hệ thống hỏi xác nhận: "Khách còn 200,000đ trong ví. Xác nhận xóa mềm?"
    When nhân viên xác nhận
    Then khách được đánh dấu "Ngưng hoạt động" (không xóa khỏi DB)
    And ví bị khóa
    And lịch sử đơn hàng vẫn được giữ nguyên

  @SD-015 @negative @frozen-balance
  Scenario: Thanh toán khi ví có số dư đóng băng (cọc)
    Given khách có ví: số dư = 1,000,000đ, đóng băng = 500,000đ (cọc KS)
    And số dư khả dụng = 500,000đ
    When khách thanh toán mua vé 600,000đ bằng ví RFID
    Then hệ thống thông báo "Số dư khả dụng không đủ. Khả dụng: 500,000đ. Cần: 600,000đ"
    And ví KHÔNG bị trừ
