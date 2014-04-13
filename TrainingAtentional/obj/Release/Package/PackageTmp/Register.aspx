<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TrainingAtentional.Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/js.js" type="text/javascript"></script>
    <link href="Style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function btnRegister_ClientClick() {
            var viewportDimensions = getViewportDimensions();
            document.getElementById('<%=hScreenWidth.ClientID %>').value = viewportDimensions.Width;
            document.getElementById('<%=hScreenHeight.ClientID %>').value = viewportDimensions.Height;
            document.getElementById('<%=hScreenDPI.ClientID %>').value = screen.deviceXDPI;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" ></asp:Label>
    <br />
    <table>
        <tr>
            <td>Nume:</td>
            <td><asp:TextBox ID="txtLastName" runat="server" Text=""></asp:TextBox> </td>
        </tr>
         <tr>
            <td>Prenume:</td>
            <td> <asp:TextBox ID="txtFirstName" runat="server" Text=""></asp:TextBox> </td>
        </tr>
        <tr>
            <td>Sex:</td>
            <td> 
                <asp:DropDownList ID="ddlGender" runat="server"  Width="100%" >
                    <asp:ListItem Text="--Alegeti--" Value="0" Selected="True"/>
                    <asp:ListItem Text="F" Value="1" Selected="False"/>
                    <asp:ListItem Text="M" Value="2" Selected="False"/>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Varsta(ani):</td>
            <td> <asp:TextBox ID="txtAge" runat="server" Text=""> </asp:TextBox> </td>
        </tr>
         <tr>
            <td>Mana dominanta:</td>
            <td> 
                <asp:DropDownList ID="ddlDominantHand" runat="server" Width="100%">
                    <asp:ListItem Text="--Alegeti--" Value="0" Selected="True"/>
                    <asp:ListItem Text="Stanga" Value="1" Selected="False"/>
                    <asp:ListItem Text="Dreapta" Value="2" Selected="False" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Interfata periferica</td>
            <td> 
                <asp:DropDownList ID="ddlPerifericInterface" runat="server" Width="100%">
                    <asp:ListItem Text="--Alegeti--" Value="0" Selected="True"/>
                    <asp:ListItem Text="Mouse" Value="1" Selected="False"/>
                    <asp:ListItem Text="TouchPad" Value="2" Selected="False" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Utilizator: </td>
            <td> 
                <asp:TextBox ID="txtUser" runat="server" Text=""></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td>Parola: </td>
            <td> 
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Text=""></asp:TextBox> 
            </td>
        </tr>
         <tr>
            <td>Cod: </td>
            <td> 
                <asp:TextBox ID="txtCode" runat="server" TextMode="Password"  Text=""></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td>&nbsp</td>
            <td align="left"><asp:Button ID="btnRegister" runat="server" Text="Inregistrare" onclick="btnRegister_Click" OnClientClick="btnRegister_ClientClick();" /></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp</td>
        </tr>
        <tr>
            <td>&nbsp</td>
            <td align="left">
                <asp:HyperLink ID="lnkRedirectLogin" runat="server" NavigateUrl="~/Login.aspx" >Aveti deja cont?</asp:HyperLink>        
            </td>
        </tr>
    </table>

    <br />
    
    <div class="divProbeText" >

        <p>
            IMPORTANT- Tine minte (sau noteaza) campurile "Utilizator" si "Parola" folosite acum la inregistrare, intrucat este posibil ca aplicatia sa ti le ceara la inceputul anumitor probe.
        </p>

        <p>
            Inainte de a incepe asigura-te ca:
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

        <asp:HiddenField ID="hScreenWidth" runat="server" />
        <asp:HiddenField ID="hScreenHeight" runat="server" />
        <asp:HiddenField ID="hScreenDPI" runat="server" />
    </div>
    </form>
</body>
</html>

