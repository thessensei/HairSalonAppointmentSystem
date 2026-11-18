# Kuaför Randevu Sistemi

Personel, randevu ve hizmet fiyatlarını yerel SQLite veritabanında tutan Türkçe bir Windows Forms **demo** uygulamasıdır.

## İndir ve çalıştır

**[Windows için hazır ZIP’i indir](dagitim/KuaforRandevuSistemi-Windows.zip?raw=1)** · Windows 10/11 (x86 veya x64) · .NET Framework 4.8 veya 4.8.1

1. ZIP’e sağ tıklayıp **Tümünü ayıkla** deyin. EXE’yi arşivin içinden çalıştırmayın.
2. Çıkardığınız klasörde **`KuaforRandevuSistemi.exe`** dosyasına çift tıklayın. DLL’leri, yapılandırma dosyasını ve `x86` / `x64` klasörlerini EXE’nin yanında bırakın.
3. **Said** kullanıcısını seçin, şifre olarak **`Said123`** girin ve **Giriş yap** düğmesine basın. Diğer demo hesapları: Yasin / `Yasin123`, Müdür / `Müdür123`.

Visual Studio, .NET SDK, NuGet veya MySQL kurmanız gerekmez. Windows eksik bir .NET Framework sürümü bildirirse Microsoft’un [.NET Framework 4.8 Runtime](https://dotnet.microsoft.com/download/dotnet-framework/net48) paketini kullanın. GitHub’daki **Code → Download ZIP** ise çalıştırılacak paket değil, kaynak kod arşividir.

**Yalnızca örnek verilerle deneyin.** Demo şifreleri sabittir ve veritabanını şifrelemez. Bu sürüm gerçek müşterilerin TC kimlik, adres veya telefon bilgilerini saklamak için uygun değildir.

## Kullanım

- **Personel ve randevu takibi:** Kayıt ekleyebilir, satır seçerek güncelleyebilir veya silebilirsiniz. Bir TC kimlik numarasıyla en fazla bir personel ve bir randevu kaydı tutulur. Fotoğraflar yalnızca önizleme içindir.
- **Fiyat listesi:** İlk açılışta boştur. Hizmet adını ve TL tutarını girip **Kaydet** deyin. Aynı adı kullanarak fiyatı güncelleyebilir veya seçili hizmeti silebilirsiniz. Fiyatlar kuruş hassasiyetinde saklanır.
- **Cilt bakımı ve hakkımızda:** Bilgilendirme ekranlarıdır; tıbbi tavsiye verilmez.

## Kayıtlar, yedekleme ve sürüm güncelleme

Veritabanı ilk çalıştırmada `%LOCALAPPDATA%\KuaforRandevuSistemi\kuafor.db` konumunda oluşturulur; ZIP’in içinde kullanıcı verisi yoktur.

Güncellemek için **uygulamayı kapatın**, `kuafor.db` dosyasını güvenli bir yere yedekleyin ve yeni ZIP’i **ayrı bir klasöre** çıkarın. Yeni EXE, aynı Windows hesabında mevcut veritabanını açar. Personel ve randevu tabloları silinmez; eksik fiyat tablosu otomatik eklenir. Önceki MySQL sürümündeki kayıtlar otomatik aktarılmaz.

## Geliştiriciler için

Windows’ta Visual Studio 2022 veya üzeri, **.NET masaüstü geliştirme** iş yükü ve **.NET Framework 4.8 hedefleme paketi** ile `KUAFÖR RANDEVU SİSTEMİ.sln` çözümünü açın. Derleme sırasında `System.Data.SQLite.Core` geri yüklenir; bağımlılık sürümleri `packages.lock.json` dosyasındadır.

Hazır ZIP’i yenilemek için deponun kökünde **Developer PowerShell for Visual Studio** üzerinden:

```powershell
msbuild "KUAFÖR RANDEVU SİSTEMİ.csproj" -restore -t:Paketle -p:Configuration=Release -p:RestoreLockedMode=true
```

Çıktı, yukarıdaki indirme bağlantısının kullandığı `dagitim/KuaforRandevuSistemi-Windows.zip` dosyasıdır. Paketleme hedefi yalnızca Release EXE’sini, yapılandırmasını, yönetilen ve yerel SQLite DLL’lerini ve `KULLANIM.txt` dosyasını alır. Yeni bir sürüm paylaşırken ZIP’i de yenileyin. Windows’ta paketi doğrulamak için `./paket/Test-Paket.ps1` çalıştırabilirsiniz. **Actions → Windows paketi** iş akışı da her değişiklikte paylaşılan ZIP’i ve kaynaktan derlediği yeni ZIP’i sınar; başarılı çalıştırmanın **Artifacts** bölümünde o değişikliğe ait ZIP bulunur.

Hata bildirimleri ve katkılar için issue veya pull request açabilirsiniz. Veritabanlarını ve kişisel bilgileri paylaşmayın.

Proje sahibi: [thessensei](https://github.com/thessensei)
