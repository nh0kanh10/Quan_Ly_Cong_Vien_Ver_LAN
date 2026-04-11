import sys

file_path = 'DAL/DataQuanLyDaiNam.designer.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Patch CauHinhNgayLe Fields
text = text.replace('private System.DateTime _Ngay;', 'private int _Id;\n\t\tprivate System.DateTime _NgayBatDau;\n\t\tprivate System.DateTime _NgayKetThuc;\n\t\tprivate string _MoTa;')

# 2. Patch CauHinhNgayLe Properties
prop_ngay = '''\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_Ngay\", DbType=\"Date NOT NULL\", IsPrimaryKey=true)]
\t\tpublic System.DateTime Ngay
\t\t{
\t\t\tget
\t\t\t{
\t\t\t\treturn this._Ngay;
\t\t\t}
\t\t\tset
\t\t\t{
\t\t\t\tif ((this._Ngay != value))
\t\t\t\t{
\t\t\t\t\tthis.OnNgayChanging(value);
\t\t\t\t\tthis.SendPropertyChanging();
\t\t\t\t\tthis._Ngay = value;
\t\t\t\t\tthis.SendPropertyChanged(\"Ngay\");
\t\t\t\t\tthis.OnNgayChanged();
\t\t\t\t}
\t\t\t}
\t\t}'''

new_props = '''\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_Id\", AutoSync=AutoSync.OnInsert, DbType=\"Int NOT NULL IDENTITY\", IsPrimaryKey=true, IsDbGenerated=true)]
\t\tpublic int Id { get { return this._Id; } set { this._Id = value; } }
\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_NgayBatDau\", DbType=\"Date NOT NULL\")]
\t\tpublic System.DateTime NgayBatDau { get { return this._NgayBatDau; } set { this._NgayBatDau = value; } }
\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_NgayKetThuc\", DbType=\"Date NOT NULL\")]
\t\tpublic System.DateTime NgayKetThuc { get { return this._NgayKetThuc; } set { this._NgayKetThuc = value; } }
\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_MoTa\", DbType=\"NVarChar(500)\")]
\t\tpublic string MoTa { get { return this._MoTa; } set { this._MoTa = value; } }'''

text = text.replace(prop_ngay, new_props)

# 3. Patch BangGia Fields
text = text.replace('private decimal _GiaNgayThuong;', 'private decimal _GiaBan;\n\t\tprivate string _LoaiGiaApDung;\n\t\tprivate System.Nullable<int> _IdNgayLe;')
text = text.replace('\t\tprivate decimal _GiaCuoiTuan;\n', '')
text = text.replace('\t\tprivate decimal _GiaNgayLe;\n', '')

# 4. Patch BangGia Properties
prop_gia_thuong = '''\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_GiaNgayThuong\", DbType=\"Decimal(15,0) NOT NULL\")]
\t\tpublic decimal GiaNgayThuong
\t\t{
\t\t\tget
\t\t\t{
\t\t\t\treturn this._GiaNgayThuong;
\t\t\t}
\t\t\tset
\t\t\t{
\t\t\t\tif ((this._GiaNgayThuong != value))
\t\t\t\t{
\t\t\t\t\tthis.OnGiaNgayThuongChanging(value);
\t\t\t\t\tthis.SendPropertyChanging();
\t\t\t\t\tthis._GiaNgayThuong = value;
\t\t\t\t\tthis.SendPropertyChanged(\"GiaNgayThuong\");
\t\t\t\t\tthis.OnGiaNgayThuongChanged();
\t\t\t\t}
\t\t\t}
\t\t}'''

prop_gia_cuoi_tuan = '''\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_GiaCuoiTuan\", DbType=\"Decimal(15,0) NOT NULL\")]
\t\tpublic decimal GiaCuoiTuan
\t\t{
\t\t\tget
\t\t\t{
\t\t\t\treturn this._GiaCuoiTuan;
\t\t\t}
\t\t\tset
\t\t\t{
\t\t\t\tif ((this._GiaCuoiTuan != value))
\t\t\t\t{
\t\t\t\t\tthis.OnGiaCuoiTuanChanging(value);
\t\t\t\t\tthis.SendPropertyChanging();
\t\t\t\t\tthis._GiaCuoiTuan = value;
\t\t\t\t\tthis.SendPropertyChanged(\"GiaCuoiTuan\");
\t\t\t\t\tthis.OnGiaCuoiTuanChanged();
\t\t\t\t}
\t\t\t}
\t\t}'''

prop_gia_ngay_le = '''\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_GiaNgayLe\", DbType=\"Decimal(15,0) NOT NULL\")]
\t\tpublic decimal GiaNgayLe
\t\t{
\t\t\tget
\t\t\t{
\t\t\t\treturn this._GiaNgayLe;
\t\t\t}
\t\t\tset
\t\t\t{
\t\t\t\tif ((this._GiaNgayLe != value))
\t\t\t\t{
\t\t\t\t\tthis.OnGiaNgayLeChanging(value);
\t\t\t\t\tthis.SendPropertyChanging();
\t\t\t\t\tthis._GiaNgayLe = value;
\t\t\t\t\tthis.SendPropertyChanged(\"GiaNgayLe\");
\t\t\t\t\tthis.OnGiaNgayLeChanged();
\t\t\t\t}
\t\t\t}
\t\t}'''

new_bang_gia_props = '''\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_GiaBan\", DbType=\"Decimal(15,0) NOT NULL\")]
\t\tpublic decimal GiaBan { get { return this._GiaBan; } set { this._GiaBan = value; } }
\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_LoaiGiaApDung\", DbType=\"NVarChar(20) NOT NULL\")]
\t\tpublic string LoaiGiaApDung { get { return this._LoaiGiaApDung; } set { this._LoaiGiaApDung = value; } }
\t\t[global::System.Data.Linq.Mapping.ColumnAttribute(Storage=\"_IdNgayLe\", DbType=\"Int\")]
\t\tpublic System.Nullable<int> IdNgayLe { get { return this._IdNgayLe; } set { this._IdNgayLe = value; } }'''

text = text.replace(prop_gia_thuong, new_bang_gia_props)
text = text.replace(prop_gia_cuoi_tuan, '')
text = text.replace(prop_gia_ngay_le, '')

# Optional: Disable OnExtensibility logic causing compilation issues
text = text.replace('partial void OnNgayChanging(System.DateTime value);', '')
text = text.replace('partial void OnNgayChanged();', '')
text = text.replace('partial void OnGiaNgayThuongChanging(decimal value);', '')
text = text.replace('partial void OnGiaNgayThuongChanged();', '')
text = text.replace('partial void OnGiaCuoiTuanChanging(decimal value);', '')
text = text.replace('partial void OnGiaCuoiTuanChanged();', '')
text = text.replace('partial void OnGiaNgayLeChanging(decimal value);', '')
text = text.replace('partial void OnGiaNgayLeChanged();', '')

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)
