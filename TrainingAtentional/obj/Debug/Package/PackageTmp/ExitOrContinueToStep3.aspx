<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExitOrContinueToStep3.aspx.cs" Inherits="TrainingAtentional.ExitOrContinueToStep3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div>
    
<br />
<br />
<br />

<div align="center">


Ați ajuns la finalul acestei probe, continuați imediat cu ultima probă accesand linkul de mai jos.

<br />
<br />

    <asp:HyperLink ID="linkDotProbe" NavigateUrl="~/DotProbe.aspx" runat="server">Catre Dot Probe</asp:HyperLink>

<br />
<br />
  <%--  <asp:Button ID="btnLogOut" runat="server" Text="Delogare" 
        onclick="btnLogOut_Click" />--%>

</div>


    </div>

</asp:Content>
