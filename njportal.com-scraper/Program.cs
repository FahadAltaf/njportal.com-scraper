
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using PuppeteerExtraSharp;
using PuppeteerExtraSharp.Plugins.ExtraStealth;
using PuppeteerSharp;
using System.ComponentModel.DataAnnotations.Schema;

List<string> ids = new List<string>() {

    "450762200",
"450766804",
"450767762",
"450768849",
"450767379",
"450767123",
"450763599",
"450763796",
"450769580",
"450774026",
"450775920",
"450775919",
"450772141",
"450773697",
"450770575",
"450762520",
"450770267",
"450772977",
"450777074"
};

var extra = new PuppeteerExtra();
extra.Use(new StealthPlugin());
var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
var browser = extra.LaunchAsync(new LaunchOptions()
{
    Headless = false,
    DefaultViewport = null
}).Result;

var page =await browser.NewPageAsync();

foreach (var id in ids)
{
    await page.GoToAsync("https://www.njportal.com/DOR/BusinessNameSearch/Search/EntityId");
    await page.WaitForTimeoutAsync(2000);

    string append = "";
    if (id.Length != 10)
    {
        
        for (int i = 0; i < 10-id.Length; i++)
        {
            append += "0";
        }
    }
    await page.TypeAsync("#EntityId", $"{append}{id}", new PuppeteerSharp.Input.TypeOptions { Delay = 100 });
    await page.ClickAsync("div.form-actions > input");
    await page.WaitForTimeoutAsync(3000);

    var doc = new HtmlDocument();
    doc.LoadHtml(await page.GetContentAsync());

    var node = doc.DocumentNode.QuerySelector("#DataTables_Table_0 > tbody > tr");
    if (node != null)
    {
        NewJerseyDatasets entry = new NewJerseyDatasets() { BusinessID=id };
        entry.BusinessName = doc.DocumentNode.QuerySelector("#DataTables_Table_0 > tbody > tr > td").InnerText;
        entry.TypeCode = doc.DocumentNode.QuerySelector("#DataTables_Table_0 > tbody > tr > td:nth-child(4)").InnerText;
        entry.FilingDate = doc.DocumentNode.QuerySelector("#DataTables_Table_0 > tbody > tr > td:nth-child(5)").InnerText;

    }
    else
    {
        Console.WriteLine("No record found");
    }
}


Console.ReadKey();

public class NewJerseyDatasets
{
    public Int64 Id { get; set; }
    public string BusinessID { get; set; } = "";
    public string BusinessName { get; set; } = "";
    public string Status { get; set; } = "";
    public string FilingDate { get; set; } = "";
    public string TypeCode { get; set; } = "";
    public string StateDomicile { get; set; } = "";
    public string RegAgent { get; set; } = "";
    public string RegAgentAddress { get; set; } = "";
    public string RegAgentAddress2 { get; set; } = "";
    public string RegAgentOfficeCity { get; set; } = "";
    public string RegAgentOfficeState { get; set; } = "";
    public string RegAgentOfficeZip { get; set; } = "";
    public string RegAgentOfficeZipExt { get; set; } = "";
    public string MBAddress { get; set; } = "";
    public string MBAddress2 { get; set; } = "";
    public string MBCity { get; set; } = "";
    public string MBState { get; set; } = "";
    public string MBZip { get; set; } = "";
    public string MBZipExt { get; set; } = "";
    public string MBProvince { get; set; } = "";
    public string MBFrZip { get; set; } = "";
    public string MBCountry { get; set; } = "";
    public string PrincipalAddress { get; set; } = "";
    public string PrincipalAddress2 { get; set; } = "";
    public string PrincipalCity { get; set; } = "";
    public string PrincipalState { get; set; } = "";
    public string PrincipalZip { get; set; } = "";
    public string PrincipalZipExt { get; set; } = "";
    public string LastAnnualRptFiled { get; set; } = "";

    public string OfficerName1 { get; set; } = "";
    public string OfficerTitle1 { get; set; } = "";
    public string OfficerAddress1 { get; set; } = "";
    public string OfficerCity1 { get; set; } = "";
    public string OfficerState1 { get; set; } = "";
    public string OfficerZip1 { get; set; } = "";

    public string OfficerName2 { get; set; } = "";
    public string OfficerTitle2 { get; set; } = "";
    public string OfficerAddress2 { get; set; } = "";
    public string OfficerCity2 { get; set; } = "";
    public string OfficerState2 { get; set; } = "";
    public string OfficerZip2 { get; set; } = "";

    public string OfficerName3 { get; set; } = "";
    public string OfficerTitle3 { get; set; } = "";
    public string OfficerAddress3 { get; set; } = "";
    public string OfficerCity3 { get; set; } = "";
    public string OfficerState3 { get; set; } = "";
    public string OfficerZip3 { get; set; } = "";
    public string Logs { get; set; } = "";
}