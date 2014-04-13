<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prepare.aspx.cs" Inherits="TrainingAtentional.Prepare" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="divFirstTime" runat="server" class="divProbeText">



<p>Urmează să treceți prin trei probe la calculator.</p>

<p>Va rugăm să le tratați cu atenție și seriozitate.</p>

<p><b>Citiți de fiecare dată instrucțiunile cu atenție!</b></p>


<p>Înainte de a incepe prima proba va rugăm să completați două scale scurte</p>

<p>Pentru a completa scalele dati click pe linkul de mai jos:</p>
    <p><asp:HyperLink ID="linkFormular" Text="Catre scale" runat="server" NavigateUrl="https://docs.google.com/spreadsheet/embeddedform?formkey=dG9hX2pESnNiamdITnZEa2w5SmZTRHc6MQ" Target="_blank"></asp:HyperLink>
    </p>

<p>După ce ați completat ambele scale reveniți la aceasta pagină și dați START pentru a începe proba la calculator</p>

    <asp:Button ID="btnStart" runat="server" Text="START" 
        onclick="btnStart_Click" />

</div>



</asp:Content>
