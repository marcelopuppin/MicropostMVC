using System;
using System.IO;
using System.Web;
using System.Web.Optimization;
using CoffeeSharp;

public class CoffeeCompiler : IBundleTransform
{
    public void Process(BundleContext context, BundleResponse response)
    {
        var coffeeScriptEngine = new CoffeeScriptEngine(); 
        string compiledCoffeeScript = String.Empty; 
        foreach (var file in response.Files)
        {
            using (var reader = new StreamReader(file.FullName))
            {
                string code = reader.ReadToEnd();
                compiledCoffeeScript += coffeeScriptEngine.Compile(code); 
                reader.Close();
            }
        } 
        response.Content = compiledCoffeeScript; 
        response.ContentType = "text/javascript"; response.Cacheability = HttpCacheability.Public;
    }
}