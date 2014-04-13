<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="TrainingResults.aspx.cs" Inherits="TrainingAtentional.TrainingResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="masterBody" runat="server">
    <br />
<br />
<div align="center">
<table>
    <tr>
        <td>Utilizatori:</td>
        <td><asp:DropDownList ID="ddlUsers" runat="server" DataSourceID="sqlDsUsers" 
                DataTextField="FullName" DataValueField="ID" AutoPostBack="True" Width="100%"></asp:DropDownList>
            
            <asp:SqlDataSource ID="sqlDsUsers" runat="server" 
                ConnectionString="<%$ ConnectionStrings:TrainingAtentionalConnectionString %>" 
                SelectCommand="select '[Toti]' as FullName,0 as ID union SELECT FirstName + ' ' + LastName as FullName, [ID] FROM [Users]"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td>Sesiune:</td>
        <td><asp:DropDownList ID="ddlSessions" runat="server" DataSourceID="sqlDsSessions" 
                DataTextField="SessionID" DataValueField="SessionID" AutoPostBack="True" Width="100%"></asp:DropDownList>
            
            <asp:SqlDataSource ID="sqlDsSessions" runat="server" 
                ConnectionString="<%$ ConnectionStrings:TrainingAtentionalConnectionString %>" 
                SelectCommand="select '[Toate]' as SessionID union SELECT distinct [SessionID] FROM [TrainingTrails] where userID = @UserID   ">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlUsers" PropertyName="SelectedValue" Name="UserID" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>

</table>
<br />
<br />
    <asp:GridView ID="gridTrainingTrails" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sqlDsTrainingTrails" 
        Caption="Training" AllowPaging="True" EnableModelValidation="True" 
        PageSize="202" AllowSorting="True">
        
        <Columns>
            <asp:BoundField DataField="FullName" HeaderText="Nume" 
                SortExpression="FullName" />
            <asp:BoundField DataField="SessionID" HeaderText="SessionID" 
                SortExpression="SessionID" />
            <asp:BoundField DataField="Block" HeaderText="Bloc" SortExpression="Block" />
            <asp:BoundField DataField="IsTest" HeaderText="De Test" 
                SortExpression="IsTest" />
            <asp:BoundField DataField="Code" HeaderText="Cod" SortExpression="Code" />
            <asp:BoundField DataField="Gender" HeaderText="Sex" SortExpression="Gender" />
            <asp:BoundField DataField="Emotion" HeaderText="Emotie" 
                SortExpression="Emotion" />
            <asp:BoundField DataField="Duration" HeaderText="Durata" ReadOnly="True" 
                SortExpression="Duration" />
            <asp:BoundField DataField="PhotoPosition" HeaderText="Pozitie(x din 16)" 
                ReadOnly="True" SortExpression="PhotoPosition" />
            <asp:BoundField DataField="IsCentralPosition" HeaderText="E central" 
                ReadOnly="True" SortExpression="IsCentralPosition" />
            <asp:BoundField DataField="WrongPhotoName" HeaderText="Nume gresit" 
                SortExpression="WrongPhotoName" />
            <asp:BoundField DataField="WrongPhotoPosition" HeaderText="pozitie gresita" 
                ReadOnly="True" SortExpression="WrongPhotoPosition" />
            <asp:BoundField DataField="WrongDuration" HeaderText="durata gresita" 
                ReadOnly="True" SortExpression="WrongDuration" />
        </Columns>
    </asp:GridView>
    
    <asp:SqlDataSource ID="sqlDsTrainingTrails" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TrainingAtentionalConnectionString %>" 
        SelectCommand="sp_TrainingTrails_Select" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlUsers" Name="UserID" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlSessions" Name="SessionID" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>


<br />
<br />

    <asp:GridView ID="gridBreaks" runat="server" AutoGenerateColumns="False" 
        Caption="Pauze" DataSourceID="sqlDsTrailBreaks" EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="FullName" HeaderText="Nume utilizator" 
                SortExpression="FullName" />
            <asp:BoundField DataField="SessionID" HeaderText="SessionID" 
                SortExpression="SessionID" />
            <asp:BoundField DataField="Duration" HeaderText="Durata pauza" ReadOnly="True" 
                SortExpression="Duration" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="sqlDsTrailBreaks" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TrainingAtentionalConnectionString %>" 
        SelectCommand="sp_TrainingBreaks_Select" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlUsers" Name="UserID" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlSessions" Name="SessionID" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

<br />
<br />

    <asp:GridView ID="gridDotTrials" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sqlDsDotTrails"  Caption="Dots" AllowPaging="True" 
        PageSize="306" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="FullName" HeaderText="Nume" ReadOnly="True" 
                SortExpression="FullName" />
            <asp:BoundField DataField="SessionID" HeaderText="SessionID" 
                SortExpression="SessionID" />
            <asp:BoundField DataField="IsTest" HeaderText="E test" 
                SortExpression="IsTest" />
            <asp:BoundField DataField="Code" HeaderText="Cod" SortExpression="Code" />
            <asp:BoundField DataField="Gender" HeaderText="Sex" SortExpression="Gender" />
            <asp:BoundField DataField="Emotion" HeaderText="Emotie" 
                SortExpression="Emotion" />
            <asp:BoundField DataField="Duration" HeaderText="Durata" ReadOnly="True" 
                SortExpression="Duration" />
            <asp:BoundField DataField="PhotoOrientation" HeaderText="Orientare poza" 
                SortExpression="PhotoOrientation" />
            <asp:BoundField DataField="IsCongruent" HeaderText="E congruent" 
                SortExpression="IsCongruent" />
            <asp:BoundField DataField="TargetType" HeaderText="Tip target" 
                SortExpression="TargetType" />
            <asp:BoundField DataField="KeyPressed" HeaderText="Tasta apasata" />
            <asp:BoundField DataField="WrongDuration" HeaderText="Durata gresita" 
                ReadOnly="True" SortExpression="WrongDuration" />
            <asp:BoundField DataField="WrongKeyPressed" HeaderText="Tasta gresita" />
            <asp:BoundField DataField="Stage" HeaderText="Proba" />
            <asp:BoundField DataField="ComplexEmotion" HeaderText="ComplexEmotion" 
                SortExpression="ComplexEmotion" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="sqlDsDotTrails" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TrainingAtentionalConnectionString %>" 
        SelectCommand="sp_DotTrials_Select" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlUsers" Name="UserID" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlSessions" Name="SessionID" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

</div>
</asp:Content>
