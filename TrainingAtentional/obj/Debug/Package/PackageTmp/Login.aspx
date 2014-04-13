<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TrainingAtentional.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">

    

    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" ></asp:Label>
    <br />
    <table>
        <tr>
            <td>Utilizator: </td>
            <td> 
                <asp:TextBox ID="txtUser" runat="server" Text=""></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td>Parola: </td>
            <td> 
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Text="parola" Width="95%"></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td>&nbsp</td>
            <td align="left"><asp:Button ID="btnLogin" runat="server" Text="Logare" 
            onclick="btnLogin_Click" /></td>
        </tr>
        
    </table>

<br />
<br />
    <div class="divProbeText" >

        <p>
            <b>IMPORTANT- Daca nu ai terminat toate cele 3 probe , dar iti apare aceasta pagina, te rugam sa te loghezi si sa continui cu urmatoarea proba.</b>
        </p>
<br />


        <p>
            IMPORTANT- Inainte de a incepe asigura-te ca:
        </p>
        <p>
            (1)	ai <b>inchis</b> toate softurile si aplicatiile active (ex., messenger, facebook, skype, internet explorer, alte browsere, etc.) 
        </p>
        <p>
            (2)	ca <b>nu o sa fii deranjat</b> de nimeni/nimic in urmatoarele minute (ex., inchide telefonul, opreste muzica, închide ferestrele. E necesar un mediu linistit)
        </p>
        <p>
            (3)	Reveniți după ce ați inchis softurile si aplicatiile <b>active, muzica,telefonul etc.</b>
        </p>

    </div>
     

    </div>
    </form>
</body>
</html>

