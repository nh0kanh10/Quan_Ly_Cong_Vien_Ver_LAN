import re

file_path = r'c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS\Sprint3_DefectList_QA.csv'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

replacements = [
    (r'Hệ thống xóa dòng có SL = 0 hoặc cảnh báo ""Giỏ hàng trống"". Không cho thanh toán.', r'Hệ thống hiển thị cảnh báo Giỏ hàng trống và chặn thanh toán.'),
    (r'SP hiện giá 0đ hoặc thông báo ""Chưa có giá"". Khi thêm giỏ, cảnh báo không cho bán sản phẩm giá 0.', r'Hệ thống hiển thị cảnh báo chưa có giá.'),
    (r'Con trỏ nằm tại nút thanh toán hoặc ô tìm kiếm.', r'Con trỏ chuột bị dừng sai vị trí ở nút thanh toán.'),
    (r'Click chuột vào panel feedback hoặc card ticket info.', r'Click chuột vào một vùng bất kỳ trên màn hình.'),
    (r'kiểm tra quota → Hợp lệ hoặc Hết số lượng.', r'kiểm tra số lượng đoàn còn lại và báo Hợp lệ.'),
    (r'Trường Combo hiện ""trống"" hoặc trống hoàn toàn.', r'Trường Combo bị khóa trắng hoàn toàn.'),
    (r'tính năng phím tắt không bật cho tab này hoặc chưa gắn sự kiện nút nhấn.', r'tính năng phím tắt F12 chưa được kết nối với hệ thống.'),
]

for old, new in replacements:
    content = content.replace(old, new)
    
# Remove any other rogue 'hoặc' that I might have missed and just keep it simple, but strictly these strings are the problem.
with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)

print('Cleaned up remaining hoc.')
