<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="TrainingAtentional.Admin.UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="masterBody" runat="server">

<br />
<br />
<br />

    <asp:GridView ID="gridUsers" runat="server" 
        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sqlDsUsers" 
        Caption="Utilizatori" >
        <Columns>
            <asp:BoundField DataField="FirstName" HeaderText="Prenume" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Nume" 
                SortExpression="LastName" />
            <asp:BoundField DataField="Age" HeaderText="Varsta" SortExpression="Age" />
            <asp:BoundField DataField="gender" HeaderText="Sex" SortExpression="gender" />
            <asp:BoundField DataField="hand" HeaderText="Mana dominanta" 
                SortExpression="hand" />
            <asp:BoundField DataField="perifericInterface" 
                HeaderText="Interfata periferica" SortExpression="perifericInterface" />
            <asp:BoundField DataField="UserName" HeaderText="Alias" 
                SortExpression="UserName" />
            <asp:BoundField DataField="CreateDate" HeaderText="Data creare cont" 
                SortExpression="CreateDate" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="sqlDsUsers" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TrainingAtentionalConnectionString %>" 
        SelectCommand="sp_Users_Select" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>

</asp:Content>

