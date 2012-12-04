

**************************************
Architecture 3 Tier
**************************************

DataTier, Bussiness logic layers are implemented as class libraries
Presentation Layer is a WCF application which proviced logic as services.
To interact with WCF application i have used a WPF application which act as the client.
WCF application - uses BasicHttpBinding, SOAP as the message format and HTTP protocol.




Application is developed on VisualStudio 2012 .Net Framework 4.5
You can execute WPF application but you have make sure that WCF application runs on IIS EXPRESS given by visual studio.