*Manuella steg vid deployment av Azure-projektet*

0. Ta bort befintlig resursgrupp.
1. Kör Deploy av projektet.
2. Lägg till appsettings
	2.1. Facebook-inloggningen ("Authentication:Facebook:AppId" och "Authentication:Facebook:AppSecret").
	2.2. Application Insights ("ApplicationInsights:InstrumentationKey"). 
	2.3. Google Analytics ("GoogleAnalytics:Enabled" och "GoogleAnalytics:TrackingId").
3. Konfigurera byggservern att göra en release till den nya resursgruppen.
4. Gör en release.

5. Om det är en prod-miljö som ska ha SSL-certifikat, konfigurera detta enligt https://gooroo.io/GoorooTHINK/Article/16420/Lets-Encrypt-Azure-Web-Apps-the-Free-and-Easy-Way/21872#.WZHIplFJbmF.
6. Aktivera "Always On" på webbsajten ("Web App").
