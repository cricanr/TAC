<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prepare.aspx.cs" Inherits="TrainingAtentional.Prepare" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="divFirstTime" runat="server" class="divProbeText">


    <div id="divTraining" runat="server">

<p><b>Ce implică această intervenţia psihologică prin care veţi trece şi cum vă poate ajuta?</b></p>

<p>Această intervenţie face apel la <b>atenţia dumneavoastră.</b> Mai specific, corectează atenţia dinspre comportamentele negative ale copilului dumneavoastră înspre cele pozitive. De ce este relevantă această reorientare atenţională pentru un părinte? 
</p>
<p>Un părinte care acordă mai multă atenţie comportamentelor negative ale copilului, adică vede mai mult câte „ prostii” face copilul, uită de multe ori să mai vadă şi comportamentele pozitive ale copilului, comportamente care contribuie la dezvoltarea lui ca persoană şi care, dacă sunt observate şi recompensate,  vor deveni din ce în ce mai frecvente.  Aşadar, centrarea pe comportamentele negative ale copilului poate avea un efect negativ pentru dezvoltarea acestuia, cât şi pentru relaţia părinte-copil.
</p>
<p><b>Deşi uneori conştientizăm faptul că alocăm mai multă atenţie comportamentelor negative ale copilului, tocmai pentru că atenţia este greu de controlat extern, avem nevoie de un ajutor suplimentar. Aici îşi are rolul intervenţia pe care o propunem.  Ce face, mai precis, această intervenţie? </b>
</p>
<p>Ceea ce face este  ca, prin intermediul unor exerciții repetate, foarte simple, ne antrenează atenția la nivel automat, astfel încât să ne fie mai uşor să ne decentrăm de pe stimulii negativi (ex.feţe furioase ale copiilor) spre cei pozitivi (ex.feţe fericite). 
</p>

<p>Sarcina nu poate fi eficientă dacă nu veţi acorda atenţie instrucţiunilor primite şi nu veţi acorda atenţie stimulilor care vor apărea pe ecranul calculatorului dumneavoastră. De aceea, este important ca, pe parcursul sesiunilor de intervenţie, să acordați atenție sarcinii și să încercați să deveniți tot mai pricepută în realizarea ei. 
</p>
<p><b>                                                                  *************               </b></p>

<p>Pe parcursul urmatoarelor zile urmeaza sa treceti prin cateva probe la calculator.</p>

<p>Va rugăm să le tratați cu atenție și seriozitate.</p>

<p><b>Citiți de fiecare dată instrucțiunile cu atenție!</b></p>
        </div>

    <div id="divNontraining" runat="server">
       <p>Urmeaza o proba la calculator. </p>

        <p>Va rugăm să o tratați cu atenție și seriozitate.</p>

    </div>

<%--<p>Înainte de a incepe prima proba va rugăm să completați două scale scurte</p>

<p>Pentru a completa scalele dati click pe linkul de mai jos:</p>
    <p><asp:HyperLink ID="linkFormular" Text="Catre scale" runat="server" NavigateUrl="https://docs.google.com/spreadsheet/embeddedform?formkey=dG9hX2pESnNiamdITnZEa2w5SmZTRHc6MQ" Target="_blank"></asp:HyperLink>
    </p>

<p>După ce ați completat ambele scale reveniți la aceasta pagină și dați START pentru a începe proba la calculator</p>--%>

    <asp:Button ID="btnStart" runat="server" Text="START" 
        onclick="btnStart_Click" />

</div>



</asp:Content>
