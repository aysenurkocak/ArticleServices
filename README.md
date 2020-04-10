SQL bağlantısını windows authentication olarak `EFLibCore/DBContext.cs` ayarladık.
```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 {
  optionsBuilder.UseSqlServer(@"data source=localhost;initial catalog=ArticleDB;integrated security=True;");
 }
```
Veritabanı dosyası ana klasörün içinde **ArticleDB.bak** adıyla bulunuyor.

Proje katmanları
- **PocoLib;** Veritabanındaki tablolarımı işleyebilmek için kullanacağım objectler burada yer alıyor bi bakıma veritabanı tablolarındaki alan adları ile bir mapping yapıldı da diyebiliriz .

- **EFLibCore;**
  - **GenericRepository:** DBContext içerisinde yer alan her T tipi için CRUD(CreateReadUpdateDelete) operasyonlarını gerçekleştirir.
  - **UnitOfWork:** Repository nesnelerinin aynı DbContext örneği üzerinden işlemlerini gerçekleştirmesi ve tek bir Transaction bütünlüğü içerisinde çalışmasını sağlıyor. Zaten Unit of Work desenin temel amaçlarından birisi de bu işlem bütünlüğünü kurgulamaktır.
Buna benzer daha evvel yapmış olduğum bir proje var fakat o .Net freamework ile oluşturuldu **BIUMainBranch**. adıyla github hesabımda mevcut.
GenericRepository da genelleştirdiğim yapıyı orada her bir nesne için tanımlamışım gereksiz interface ve classlar oluşturulmuş kıyaslamak gerekirse bu projede bariz bi şekilde daha efektif bir yapı kullanmış olduk.

- **ArticleServices;** Bu projedeki sunum katmanımız .Net core ile oluşturulmuş api 


#### Örnekler:

###### Makale eklemek için kullanılan postman isteği;
![alt text](https://raw.githubusercontent.com/aysenurkocak/ArticleServices/master/ArticleServices/img/articlePost.jpg)

###### Db ye yazılmış hali;
![alt text](https://github.com/aysenurkocak/ArticleServices/blob/master/ArticleServices/img/articleSQL.jpg)

###### Makale için yapılan yorumlar;
![alt text](https://github.com/aysenurkocak/ArticleServices/blob/master/ArticleServices/img/yorumlarGet.jpg?raw=true)

###### Bazı işlemlerin logları;
![alt text](https://raw.githubusercontent.com/aysenurkocak/ArticleServices/master/ArticleServices/img/logSQL.jpg)

#### Eksikler ve yorumum
Vaktim olsaydı api içindeki her bir işleme ait örneği içeren **postman collection** oluşturmak isterdim fakat iş yoğunluğu nedeniyle yapamadım, ayrıca Authentication ve Cache işlemlerimin de eksik olduğunu farkettim özellikle Authentication genelde yazdığım servislerde olmazsa olmazlarımdan fakat saat geç olduğu için şuan ekleyemiyorum yorum listeleme işlemi çok genel olmuş makale bazında filtrelenmiş halini de ekleyebilirmişim ama yapısal olarak çok sorunlu birşey göremiyorum ayrıca güzel kurgulanmış bir değerlendirme projesi olmuş tebrik ederim 
