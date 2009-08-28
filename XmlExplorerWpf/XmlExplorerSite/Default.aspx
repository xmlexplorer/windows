<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XmlExplorerSite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XML Explorer - Home</title>
    <style type="text/css">
        <!
        -- BODY
        {
            margin-top: 20px;
            margin-left: 20px;
            margin-right: 20px;
            color: #000000;
            font-family: Tahoma;
            background-color: white;
        }
        A:link
        {
            font-weight: normal;
            color: #000066;
            text-decoration: none;
        }
        A:visited
        {
            font-weight: normal;
            color: #000066;
            text-decoration: none;
        }
        A:active
        {
            font-weight: normal;
            text-decoration: none;
        }
        A:hover
        {
            font-weight: normal;
            color: #FF6600;
            text-decoration: none;
        }
        P
        {
            margin-top: 0px;
            margin-bottom: 12px;
            color: #000000;
            font-family: Tahoma;
        }
        PRE
        {
            border-right: #f0f0e0 1px solid;
            padding-right: 5px;
            border-top: #f0f0e0 1px solid;
            margin-top: -5px;
            padding-left: 5px;
            font-size: x-small;
            padding-bottom: 5px;
            border-left: #f0f0e0 1px solid;
            padding-top: 5px;
            border-bottom: #f0f0e0 1px solid;
            font-family: Courier New;
            background-color: #e5e5cc;
        }
        TD
        {
            font-size: 12px;
            color: #000000;
            font-family: Tahoma;
        }
        H2
        {
            border-top: #003366 1px solid;
            margin-top: 25px;
            font-weight: bold;
            font-size: 1.5em;
            margin-bottom: 10px;
            margin-left: -15px;
            color: #003366;
        }
        H3
        {
            margin-top: 10px;
            font-size: 1.1em;
            margin-bottom: 10px;
            margin-left: -15px;
            color: #000000;
        }
        UL
        {
            margin-top: 10px;
            margin-left: 20px;
        }
        OL
        {
            margin-top: 10px;
            margin-left: 20px;
        }
        LI
        {
            margin-top: 10px;
            color: #000000;
        }
        FONT.value
        {
            font-weight: bold;
            color: darkblue;
        }
        FONT.key
        {
            font-weight: bold;
            color: darkgreen;
        }
        .divTag
        {
            border: 1px;
            border-style: solid;
            background-color: #FFFFFF;
            text-decoration: none;
            height: auto;
            width: auto;
            background-color: #cecece;
        }
        .BannerColumn
        {
            background-color: #000000;
        }
        .Banner
        {
            border: 0;
            padding: 0;
            height: 8px;
            margin-top: 0px;
            color: #ffffff;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#1c5280',EndColorStr='#FFFFFF');
        }
        .BannerTextCompany
        {
            font: bold;
            font-size: 18pt;
            color: #cecece;
            font-family: Tahoma;
            height: 0px;
            margin-top: 0;
            margin-left: 8px;
            margin-bottom: 0;
            padding: 0px;
            white-space: nowrap;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .BannerTextApplication
        {
            font: bold;
            font-size: 18pt;
            font-family: Tahoma;
            height: 0px;
            margin-top: 0;
            margin-left: 8px;
            margin-bottom: 0;
            padding: 0px;
            white-space: nowrap;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .BannerText
        {
            font: bold;
            font-size: 18pt;
            font-family: Tahoma;
            height: 0px;
            margin-top: 0;
            margin-left: 8px;
            margin-bottom: 0;
            padding: 0px;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .BannerSubhead
        {
            border: 0;
            padding: 0;
            height: 16px;
            margin-top: 0px;
            margin-left: 10px;
            color: #ffffff;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#4B3E1A',EndColorStr='#FFFFFF');
        }
        .BannerSubheadText
        {
            font: bold;
            height: 11px;
            font-size: 11px;
            font-family: Tahoma;
            margin-top: 1;
            margin-left: 10;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .FooterRule
        {
            border: 0;
            padding: 0;
            height: 1px;
            margin: 0px;
            color: #ffffff;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#4B3E1A',EndColorStr='#FFFFFF');
        }
        .FooterText
        {
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
            font-family: Tahoma;
            margin-top: 10;
            margin-left: 0px;
            margin-bottom: 2;
            padding: 0px;
            color: #999999;
            white-space: nowrap;
        }
        .FooterText A:link
        {
            font-weight: normal;
            color: #999999;
            text-decoration: underline;
        }
        .FooterText A:visited
        {
            font-weight: normal;
            color: #999999;
            text-decoration: underline;
        }
        .FooterText A:active
        {
            font-weight: normal;
            color: #999999;
            text-decoration: underline;
        }
        .FooterText A:hover
        {
            font-weight: normal;
            color: #FF6600;
            text-decoration: underline;
        }
        .ClickOnceInfoText
        {
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
            font-family: Tahoma;
            margin-top: 0;
            margin-right: 2px;
            margin-bottom: 0;
            padding: 0px;
            color: #000000;
        }
        .InstallTextStyle
        {
            font: bold;
            font-size: 14pt;
            font-family: Tahoma;
            a: #FF0000;
            text-decoration: None;
        }
        .DetailsStyle
        {
            margin-left: 30px;
        }
        .ItemStyle
        {
            margin-left: -15px;
            font-weight: bold;
        }
        .StartColorStr
        {
            background-color: #4B3E1A;
        }
        .JustThisApp A:link
        {
            font-weight: normal;
            color: #000066;
            text-decoration: underline;
        }
        .JustThisApp A:visited
        {
            font-weight: normal;
            color: #000066;
            text-decoration: underline;
        }
        .JustThisApp A:active
        {
            font-weight: normal;
            text-decoration: underline;
        }
        .JustThisApp A:hover
        {
            font-weight: normal;
            color: #FF6600;
            text-decoration: underline;
        }
        -- ></style>
</head>
<body>
    <table width="100%" cellpadding="0" cellspacing="2" border="0">
        <!-- Begin Banner -->
        <tr>
            <td>
                <table cellpadding="2" cellspacing="0" border="0" bgcolor="#cecece" width="100%">
                    <tr>
                        <td>
                            <table bgcolor="#1c5280" width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="Banner" />
                                </tr>
                                <tr>
                                    <td class="Banner">
                                        <span class="BannerTextApplication">XML Explorer</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Banner" align="RIGHT" />
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!-- End Banner -->
        <!-- Begin Dialog -->
        <tr>
            <td align="LEFT">
                <table cellpadding="2" cellspacing="0" border="0" width="540">
                    <tr>
                        <td width="496">
                            <!-- Begin AppInfo -->
                            <table>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Name:</b>
                                    </td>
                                    <td width="5">
                                        <spacer type="block" width="10" />
                                    </td>
                                    <td>
                                        XML Explorer
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Version:</b>
                                    </td>
                                    <td width="5">
                                        <spacer type="block" width="10" />
                                    </td>
                                    <td>
                                        3.0.0.5
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <!-- End AppInfo -->
                            <!-- Begin Prerequisites -->
                            <table id="BootstrapperSection" border="0">
                                <tr>
                                    <td colspan="2">
                                        The following prerequisites are required:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <ul>
                                            <li>Windows Installer 3.1</li>
                                            <li>.NET Framework Client Profile</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        If these components are already installed, you can <span class="JustThisApp"><a href="XmlExplorer.application">
                                            launch</a></span> the application now. Otherwise, click the button below to
                                        install the prerequisites and run the application.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <!-- End Prerequisites -->
                        </td>
                    </tr>
                </table>
                <!-- Begin Buttons -->
                <tr>
                    <td align="LEFT">
                        <table cellpadding="2" cellspacing="0" border="0" width="540" style="cursor: hand"
                            onclick="window.navigate(InstallButton.href)">
                            <tr>
                                <td align="LEFT">
                                    <table cellpadding="1" bgcolor="#333333" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="1" bgcolor="#cecece" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="1" bgcolor="#efefef" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="20">
                                                                        <spacer type="block" width="20" height="1" />
                                                                    </td>
                                                                    <td>
                                                                        <a id="InstallButton" href="setup.exe">Install</a>
                                                                    </td>
                                                                    <td width="20">
                                                                        <spacer type="block" width="20" height="1" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="15%" align="right" />
                            </tr>
                        </table>
                    </td>
                </tr>
                <!-- End Buttons -->
            </td>
        </tr>
        <!-- End Dialog -->
        <!-- Spacer Row -->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <!-- Begin Footer -->
                <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff">
                    <tr>
                        <td height="5">
                            <spacer type="block" height="5" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FooterText" align="center">
                            <a href="http://xmlexplorer.codeplex.com">Support</a> &nbsp;&nbsp;&nbsp;::&nbsp;&nbsp;&nbsp;
                            <a href="http://msdn.microsoft.com/clickonce">ClickOnce and .NET Framework Resources</a>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                            <spacer type="block" height="5" />
                        </td>
                    </tr>
                    <tr>
                        <td height="1" bgcolor="#cecece">
                            <spacer type="block" height="1" />
                        </td>
                    </tr>
                </table>
                <!-- End Footer -->
            </td>
        </tr>
    </table>
</body>
</html>
