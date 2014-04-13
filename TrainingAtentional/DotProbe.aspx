<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DotProbe.aspx.cs" Inherits="TrainingAtentional.DotProbe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" >

//    var leftKey = 'e';
//    var leftKeyUpper = 'E';
//    var rightKey = 'l';
//    var rightKeyUpper = 'L';

    var horizontalDotesKeyUpper = 'M'
    var horizontalDotesKey = 'm'
    var verticalDotsKey = 'b'
    var verticalDotsKeyUpper = 'B'


    function OnDocumentStartTrialKeyPressed(e) {

        var evtobj = window.event ? event : e //distinguish between IE's explicit event object (window.event) and Firefox's implicit.
        var unicode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode;
        var actualkey = String.fromCharCode(unicode);

        if (actualkey == ' ')
        { Step1(500,500); }
    }

    function Step1(timeInMsStep1, timeInMsStep2) {
        document.getElementById('<%=divInfo.ClientID %>').style.display = 'none';
        setTimeout(function () {
            document.getElementById('<%=img1.ClientID %>').style.display = '';
            setTimeout(function () { Step2(timeInMsStep2); }, 500);//ar fi bine si parametru pentru cross
        }, timeInMsStep1);

        
    }

    function Step2(timeInMs) {
        document.getElementById('<%=img1.ClientID %>').style.display = 'none';
        document.getElementById('<%=img2.ClientID %>').style.display = '';
        setTimeout(function () { Step3(); }, timeInMs);
    }
    function Step3() {
        document.getElementById('<%=img2.ClientID %>').style.display = 'none';
        document.getElementById('<%=img3.ClientID %>').style.display = '';

        document.onkeypress = OnDocumentKeyPress;//se pierde daca nu se reseteaza de fiecare data
        var currentTime = new Date();
        document.getElementById('<%=hStart.ClientID %>').value = getTime(currentTime);
    }


    function OnDocumentKeyPress(e) {
        var currentTime = new Date();
        document.getElementById('<%=hStop.ClientID %>').value = getTime(currentTime);

        var evtobj = window.event ? event : e //distinguish between IE's explicit event object (window.event) and Firefox's implicit.
        var unicode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode
        var actualkey = String.fromCharCode(unicode)

        //if (actualkey == leftKey || actualkey == leftKeyUpper || actualkey == rightKey || actualkey == rightKeyUpper) 

        var correctKey = horizontalDotesKey;
        var correctKeyUpper = horizontalDotesKeyUpper;

        var isVerticalDots = document.getElementById('<%=hIsVerticalDots.ClientID %>').value;
        if (isVerticalDots == "1"){
            correctKey = verticalDotsKey;
            correctKeyUpper = verticalDotsKeyUpper;
         }

     if (actualkey == correctKey || actualkey == correctKeyUpper)
        {
            __doPostBack("keyPressed|" + actualkey, '');
        }
        else {
            document.getElementById('<%=hWrongKeyPressed.ClientID %>').value = actualkey;
            document.getElementById('<%=hStopWrongKeyPressed.ClientID %>').value = document.getElementById('<%=hStop.ClientID %>').value;
            document.getElementById('<%=hStop.ClientID %>').value = 0;
        }

    }

    function getTime(date) {
        return date.getFullYear() + ';' + date.getMonth() + ';' + date.getDate() + ';' + date.getHours() + ';' + date.getMinutes() + ';' + date.getSeconds() + ';' + date.getMilliseconds();
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



<div id="divInfo" runat="server" class="divProbeText" style="display:none">

<br />
<br />

<p>
    Va recomandam sa intrati in Full Screen in timpul probelor. Puteti face asta apasand tasta F11 sau Fn F11.
</p>    

<br />

<p><b>Citiți cu Atenție Următoarele Instrucțiuni</b></p>

<p>Sarcina dumneavoastră este să apăsați cât mai REPEDE și cât mai CORECT tasta <b>B</b> pentru <big>:</big> și tasta <b>M</b> pentru  <big>..</big> Apăsați tasta <b>B</b> sau <b>M</b> doar cu DOUĂ degete de la MÂNA DOMINANTĂ.  Vă reamintim <b>B</b> pentru <big>:</big> și <b>M</b> pentru  <big>..</big></p>

<p>
    
Inainte de a incepe proba, FIXAȚI degetele de la mâna dominantă pe tastele <b>B</b> și <b>M</b> și <b>răspundeți doar când vedeți pe ecran</b> semnul <big>:</big> sau <big>..</big> . Rămâneti <b>pe tot parcursul probei</b> cu degetele fixate pe cele două taste B și M. Răspundeți doar cu DOUĂ degete de la MÂNA DOMINANTĂ.

</p>

<p>Pentru a incepe proba apăsați tasta SPATIU. </p>




<asp:Button ID="btnSave" runat="server" Text="Porneste test" OnClientClick="Step1(500,500);return false;" Style="display:none"  />

  </div>

    <div align="center" class="divDotProbePhoto">
                
                <asp:Image ID="img0" runat="server" style="display:none" ImageUrl="~/Poze/DotProbe/empty/empty.png"/>
                <asp:Image ID="img1" runat="server" style="display:none" ImageUrl="~/Poze/DotProbe/cross/cross.jpg" />
                <asp:Image ID="img2" runat="server" style="display:none" />
                <asp:Image ID="img3" runat="server" style="display:none" />
            </div>

            <asp:HiddenField ID="hStart" runat="server" />
            <asp:HiddenField ID="hStop" runat="server" />
            
            <asp:HiddenField ID="hWrongKeyPressed" runat="server" />
            <asp:HiddenField ID="hStopWrongKeyPressed" runat="server" />

            <asp:HiddenField ID="hIsVerticalDots" runat="server" />

<!--conteaza ordinea-->
<asp:Literal id="literalDocumentKeyPressed" runat="server"></asp:Literal>
<asp:Literal id="literal" runat="server"></asp:Literal>
            
</asp:Content>
