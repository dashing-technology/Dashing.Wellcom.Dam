<%@ Page Language="C#" AutoEventWireup="true" ClassName="WellcomProxyPage"%>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="System.Web.Services" %>

<!DOCTYPE html>
<script language="c#" runat="server">

    static string BaseWellcomUrl = @"http://192.168.100.228/WellcomDam/api/WellcomProduct/";    
    
    [WebMethod]
    public static string SearchProducts(string description,string code, string gtin,int batch,int batchSize)
    {
        string result = "";        
        string apiUrl =  string.Format("SearchProducts?desc={0}&code={1}&gtin={2}&batch={3}&batchSize={4}", description, code,gtin,batch,batchSize);
        WebClient client = new WebClient();
        client.Headers.Add("Content-Type:application/json");
        client.Headers.Add("Accept:application/json");
        result = client.DownloadString(BaseWellcomUrl + apiUrl); //URI 
     
        return result;
    }

    [WebMethod]
    public static string GetProduct(string uuid)
    {       
        
        string result = "";
        
            try
            {
                WebClient client = new WebClient();
                string apiUrl = string.Format("GetProduct?id={0}", uuid);
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                result = client.DownloadString(BaseWellcomUrl + apiUrl); //URI                 
               
            }
            catch (Exception e)
            {
                result = e.Message;
            }

        
        return result;
    }   

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
    </div>
</form>
</body>
</html>

