*Manuella steg vid deployment av Azure-projektet*

0. Ta bort befintlig resursgrupp.
1. Kör Deploy av projektet.
2. Lägg till appsettings
	2.1. Facebook-inloggningen ("Authentication:Facebook:AppId" och "Authentication:Facebook:AppSecret").
	2.2. Application Insights ("ApplicationInsights:InstrumentationKey"). 
	2.3. Google Analytics ("GoogleAnalytics:Enabled" och "GoogleAnalytics:TrackingId").
3. Konfigurera byggservern att göra en release till den nya resursgruppen.
4. Gör en release.
 