*Manuella steg vid deployment av Azure-projektet*

0. Ta bort befintlig resursgrupp.
1. K�r Deploy av projektet.
2. L�gg till appsettings
	2.1. Facebook-inloggningen ("Authentication:Facebook:AppId" och "Authentication:Facebook:AppSecret").
	2.2. Application Insights ("ApplicationInsights:InstrumentationKey"). 
	2.3. Google Analytics ("GoogleAnalytics:Enabled" och "GoogleAnalytics:TrackingId").
3. Konfigurera byggservern att g�ra en release till den nya resursgruppen.
4. G�r en release.

5. Om det �r en prod-milj� som ska ha SSL-certifikat, konfigurera detta enligt https://gooroo.io/GoorooTHINK/Article/16420/Lets-Encrypt-Azure-Web-Apps-the-Free-and-Easy-Way/21872#.WZHIplFJbmF.
6. Aktivera "Always On" p� webbsajten ("Web App").
