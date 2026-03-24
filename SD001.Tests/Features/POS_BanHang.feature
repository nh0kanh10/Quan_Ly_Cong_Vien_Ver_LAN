Feature: Bán hàng POS
  Là nhân viên bán vé tại quầy POS,
  tôi muốn bán vé cho khách nhanh chóng và chính xác.

  @SD-027 @happy-path
  Scenario: Bán vé thành công — thanh toán tiền mặt
    Given hệ thống có sản phẩm "Vé người lớn" mã "SP001" giá 200000
    And nhân viên đã đăng nhập
    And giỏ hàng trống
    When thêm 2 sản phẩm "SP001" vào đơn hàng
    And thanh toán bằng "TienMat"
    Then đơn hàng được tạo thành công
    And tổng tiền đơn hàng là 400000
    And kho xuất 2 sản phẩm "SP001"

  @SD-027 @security @anti-hack
  Scenario: Chống hack giá — client gửi giá 0đ
    Given hệ thống có sản phẩm "Vé VIP" mã "SP002" giá 200000
    When thêm sản phẩm "SP002" với giá client là 0
    And thanh toán bằng "TienMat"
    Then hệ thống tính lại giá từ DB là 200000
    And tổng tiền đơn hàng là 200000

  @SD-027 @negative @rfid
  Scenario: Thanh toán ví RFID — số dư không đủ
    Given hệ thống có sản phẩm "Combo VIP" mã "SP003" giá 500000
    And khách hàng "KH001" có ví số dư 100000
    When thêm 1 sản phẩm "SP003" vào đơn hàng
    And gắn khách hàng "KH001" vào đơn
    And thanh toán bằng "ViRFID"
    Then thanh toán bị từ chối vì "Số dư ví không đủ"
    And ví khách hàng vẫn còn 100000

  @SD-027 @negative
  Scenario: Số lượng không hợp lệ
    Given hệ thống có sản phẩm "Vé người lớn" mã "SP001" giá 200000
    When thêm -1 sản phẩm "SP001" vào đơn hàng
    And thanh toán bằng "TienMat"
    Then thao tác bị từ chối vì "Số lượng phải lớn hơn 0"

  @SD-027 @happy-path @loyalty
  Scenario: Tích điểm Loyalty sau khi mua
    Given hệ thống có sản phẩm "Vé người lớn" mã "SP001" giá 200000
    And khách hàng "KH002" có 1000 điểm tích lũy, hệ số nhân 2
    When thêm 2 sản phẩm "SP001" vào đơn hàng
    And gắn khách hàng "KH002" vào đơn
    And thanh toán bằng "TienMat"
    Then khách được tích thêm 800 điểm
    And tổng điểm mới là 1800
