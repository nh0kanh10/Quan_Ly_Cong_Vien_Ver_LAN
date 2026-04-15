import os
designer_path = r'c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\GUI\Staff\frmNhanVien.Designer.cs'
with open(designer_path, 'r', encoding='utf-8') as f:
    ddata = f.read()

ddata = ddata.replace('slkKhuVuc', 'slkNguoiQuanLy')
ddata = ddata.replace('gridViewKhuVuc', 'gridViewNguoiQuanLy')
ddata = ddata.replace('lblKhuVuc', 'lblNguoiQuanLy')
ddata = ddata.replace('"Khu vực:"', '"Người quản lý:"')

with open(designer_path, 'w', encoding='utf-8') as f:
    f.write(ddata)
