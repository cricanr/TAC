<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Training.aspx.cs" Inherits="TrainingAtentional.Training" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">

        function PrepareRealTrials() {

            document.getElementById('<%=divRealTrials.ClientID %>').style.display = '';
            document.getElementById('<%=divBreak.ClientID %>').style.display = 'none';
            document.getElementById('<%=divInfo.ClientID %>').style.display = 'none';

            document.onkeypress = OnDocumentStartNonTestTrialKeyPressed;
        }

        function OnDocumentStartNonTestTrialKeyPressed(e) {

            var evtobj = window.event ? event : e //distinguish between IE's explicit event object (window.event) and Firefox's implicit.
            var unicode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode;
            var actualkey = String.fromCharCode(unicode);

            if (actualkey == ' ') {
                Step1(500);
            }
        }

        function OnDocumentContinueAfterBreakKeyPress(e) {
            
            var evtobj = window.event ? event : e //distinguish between IE's explicit event object (window.event) and Firefox's implicit.
            var unicode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode
            var actualkey = String.fromCharCode(unicode)

            if (actualkey == ' ')
            { ContinueAfterBreak(); }
        }

        function OnDocumentStartTrialKeyPressed(e) {

            var evtobj = window.event ? event : e //distinguish between IE's explicit event object (window.event) and Firefox's implicit.
            var unicode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode;
            var actualkey = String.fromCharCode(unicode);

            if (actualkey == ' ')
            { Step1(500); }
        }


        function TakeBreak() {
            document.getElementById('<%=divRealTrials.ClientID %>').style.display = 'none';
            document.getElementById('<%=divBreak.ClientID %>').style.display = '';
            document.getElementById('<%=divInfo.ClientID %>').style.display = 'none';
            var currentTime = new Date();
            document.getElementById('<%=hStart.ClientID %>').value = getTime(currentTime);

            document.onkeypress = OnDocumentContinueAfterBreakKeyPress;

        }


    function ContinueAfterBreak() {
        var currentTime = new Date();
        document.getElementById('<%=hStop.ClientID %>').value = getTime(currentTime);
        __doPostBack("breakEnded|", '');
    }

    function Step1(timeInMsStep1) {

        document.getElementById('<%=divRealTrials.ClientID %>').style.display = 'none';
        document.getElementById('<%=divInfo.ClientID %>').style.display = 'none';
        document.getElementById('<%=divBreak.ClientID %>').style.display = 'none';
        document.getElementById('<%=divCross.ClientID %>').style.display = '';
        setTimeout(function () { Step2(); }, timeInMsStep1);
    }

    function Step2() {
        document.getElementById('<%=divRealTrials.ClientID %>').style.display = 'none';
        document.getElementById('<%=divCross.ClientID %>').style.display = 'none';
        document.getElementById('<%=divImages.ClientID %>').style.display = '';
        var currentTime = new Date();
        document.getElementById('<%=hStart.ClientID %>').value = getTime(currentTime);
    }

    function img_click_IsTest_CorrectPosition(clickedPosition) {
        document.getElementById('<%=divFeedback.ClientID %>').style.display = 'none';
        __doPostBack("correctPositionClicked|" + clickedPosition, '');
    }
    function img_click_IsTest_InCorrectPosition() {
        document.getElementById('<%=divFeedback.ClientID %>').style.display = 'none';
    }



    function img_click(clickedPosition) {

        var currentTime = new Date();
        document.getElementById('<%=hStop.ClientID %>').value = getTime(currentTime);

        currentPosition = document.getElementById('<%=hCurrentHPosition.ClientID %>').value
        var isTest= document.getElementById('<%=hIsTest.ClientID %>')

        if (clickedPosition == currentPosition) {
            if (isTest.value == "1") {
                document.getElementById('<%=imgFeedback.ClientID %>').src = 'Poze/Training/Validation/valid.png';

                setTimeout(function () {
                    document.getElementById('<%=divFeedback.ClientID %>').style.display = '';
                    setTimeout(function () { img_click_IsTest_CorrectPosition(clickedPosition) }, 300);
                }, 500);//era 100 

              
                
            }
            else {
                document.getElementById('<%=divFeedback.ClientID %>').style.display = 'none';
                __doPostBack("correctPositionClicked|" + clickedPosition, '');
            }
           
          }
        else {
            if (isTest.value == "1") {

                document.getElementById('<%=imgFeedback.ClientID %>').src = 'Poze/Training/Validation/error.png';

                setTimeout(function () {
                    document.getElementById('<%=divFeedback.ClientID %>').style.display = '';
                    setTimeout(function () { img_click_IsTest_InCorrectPosition() }, 100);
                }, 500);//era 100

                
               
            }
            else {
                document.getElementById('<%=divFeedback.ClientID %>').style.display = 'none';
            }

              document.getElementById('<%=hWrongPosition.ClientID %>').value = clickedPosition;
              document.getElementById('<%=hStopWrongPosition.ClientID %>').value = document.getElementById('<%=hStop.ClientID %>').value;
              document.getElementById('<%=hStop.ClientID %>').value = 0;
          }

      }

      function getTime(date) {
          return date.getFullYear() + ';' + date.getMonth() + ';' + date.getDate() + ';' + date.getHours() + ';' + date.getMinutes()+ ';' + date.getSeconds() + ';' + date.getMilliseconds();
      }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div id="divRealTrials" runat="server" style="display:none;" class="divProbeText">
<p><b>Citiți cu Atenție Următoarele Instrucțiuni</b></p>

<p>
Pe centrul ecranului va apărea o cruce. FIXAȚI  cu privirea crucea respectivă. După ce crucea dispare, un set de 16 fete va apărea pe ecran. In acel set de 16 fețe, DOAR UNA dintre ele este o față fericită. 
</p>
<p >
Sarcina dumneavoastră este să dați CLICK <big> cât mai repede și cât mai corect</big> pe FAȚA FERICITĂ. Pentru a da click pe imaginea fericită, dați CLICK STÂNGA de la mouse sau touchpad cu ajutorul indexului de la MÂNA DOMINANTĂ.
</p>
<p>
 În această fază <big>nu veți mai primi feedback</big>,  adică semnul ✓sau ✗după răspuns. Dacă răspunsul este corect veți merge mai departe, dacă este incorect nu veți merge mai departe cu proba până când veți apăsa imaginea corectă.
</p>
<p>
La mijlocul probei va fi o pauză pentru a vă odihni mâinile, pauză care poate dura doar până la <b>5 minute</b>. Pauza va fi anunțată pe ecran la momentul potrivit.
</p>
<p>
Inainte de a incepe proba, FIXAȚI mâna dominantă pe touchpad sau mouse. Răspundeți doar cu DEGETUL de la MÂNA DOMINANTĂ.
</p>

 <p>
 Pentru a incepe proba apăsați tasta <b> SPATIU </b>.
 </p> 
  <p>
         <asp:Button ID="Button1" runat="server" Text="Porneste test" OnClientClick="Step1(500);return false;" style="display:none" />
    </p>
</div>

<div id="divBreak" runat="server" style="display:none;" class="divProbeText" >

<p>PAUZĂ</p>

<p>Ați ajuns la jumătatea probei.</p>

<p>Puteți să vă  luați o pauză, insă nu puteți lua o pauză mai mare de <b> 5 minute</b>.</p>

<p>Dacă nu doriți să luați pauză, puteți apăsa tasta SPAȚIU pentru a merge mai departe.</p>

<p>APĂSAȚI TASTA SPAȚIU PENTRU A MERGE MAI DEPARTE</p>



<asp:Button ID="btnContinue" runat="server" Text="Continua test" OnClientClick="ContinueAfterBreak();return false;" style="display:none" />

</div>

<div id="divInfo" runat="server" class="divProbeText" style="display:none">
    
<p><b>Citiți cu Atenție Următoarele Instrucțiuni</b></p>

<p>
Pe centrul ecranului va apărea o cruce. FIXAȚI  cu privirea crucea respectivă. După ce crucea dispare, un set de 16 fete va apărea pe ecran. In acel set de 16 fețe, DOAR UNA dintre ele este o față fericită. 
</p>
<p>
Sarcina dumneavoastră este să dați CLICK cât mai repede și cât mai corect pe FAȚA FERICITĂ. Pentru a da click pe imaginea fericită, dați CLICK STÂNGA de la mouse sau touchpad cu ajutorul indexului de la MÂNA DOMINANTĂ.
</p>
<p>
 În această fază, când veți răspunde corect va apărea semnul ✓și veți merge mai departe cu proba. În schimb, când veți răspunde incorect va apărea semnul ✗ și nu veți merge mai departe cu proba până când veți apăsa imaginea corectă.
</p>
<p>
Inainte de a incepe proba, FIXAȚI mâna dominantă pe touchpad sau mouse. Răspundeți doar cu DEGETUL de la MÂNA DOMINANTĂ.
</p>

 <p>Pentru a incepe proba apăsați tasta SPATIU.</p> 
  <p>
         <asp:Button ID="btnSave" runat="server" Text="Porneste test" OnClientClick="Step1(500);return false;" style="display:none" />
    </p>
  
</div>



<div align="center"  runat="server" id="divCross" style="display:none;" class="divPhotoVerticalAlign" >
    <asp:Image ID="imgCross" runat="server" ImageUrl="~/Poze/Training/Cross/cruce.png"  />
</div>

<div align="center" runat="server" id="divImages" style="display:none;" class="divPhotoVerticalAlign">
<table>
    <tr>
        <td>
            <div id="div0" onclick="img_click(0);" >
                <asp:Image ID="img0" runat="server"/>
            </div>
        </td>
        <td>
        <div id="div1" onclick="img_click(1);" >
            <asp:Image ID="img1" runat="server" />
            </div>
        </td>
        <td>
        <div id="div2" onclick="img_click(2);" >
            <asp:Image ID="img2" runat="server" />
            </div>
        </td>
        <td>
        <div id="div3" onclick="img_click(3);" >
           <asp:Image ID="img3" runat="server" />
           </div>
        </td>
    </tr>

     <tr>
        <td>
        <div id="div4" onclick="img_click(4);" >
            <asp:Image ID="img4" runat="server" />
            </div>
        </td>
        <td>
        <div id="div5" onclick="img_click(5);" >
            <asp:Image ID="img5" runat="server" />
            </div>
        </td>
        <td>
        <div id="div6" onclick="img_click(6);" >
            <asp:Image ID="img6" runat="server" />
            </div>
        </td>
        <td>
        <div id="div7" onclick="img_click(7);" >
           <asp:Image ID="img7" runat="server" />
           </div>
        </td>
    </tr>

     <tr>
        <td>
        <div id="div8" onclick="img_click(8);" >
            <asp:Image ID="img8" runat="server" />
            </div>
        </td>
        <td>
        <div id="div9" onclick="img_click(9);" >
            <asp:Image ID="img9" runat="server" />
            </div>
        </td>
        <td>
        <div id="div10" onclick="img_click(10);" >
            <asp:Image ID="img10" runat="server" />
            </div>
        </td>
        <td>
        <div id="div11" onclick="img_click(11);" >
           <asp:Image ID="img11" runat="server" />
           </div>
        </td>
    </tr>

     <tr>
        <td>
        <div id="div12" onclick="img_click(12);" >
            <asp:Image ID="img12" runat="server" />
            </div>
        </td>
        <td>
        <div id="div13" onclick="img_click(13);" >
            <asp:Image ID="img13" runat="server" />
            </div>
        </td>
        <td>
        <div id="div14" onclick="img_click(14);" >
            <asp:Image ID="img14" runat="server" />
            </div>
        </td>
        <td>
        <div id="div15" onclick="img_click(15);" >
           <asp:Image ID="img15" runat="server" />
           </div>
        </td>
    </tr>
</table>
</div>

<div align="center" runat="server" id="divFeedback" style="display:none;" class="divPhotoVerticalAlign">


<asp:Image align="center" ID="imgFeedback" runat="server" class="feedbackImg" />

</div>

<asp:HiddenField ID="hStart" runat="server" />
<asp:HiddenField ID="hStop" runat="server" />
<asp:HiddenField ID="hWrongPosition" runat="server" />
<asp:HiddenField ID="hStopWrongPosition" runat="server" />
<asp:HiddenField id ="hCurrentHPosition" runat="server" /> 
<asp:HiddenField id ="hIsTest" runat="server" /> 

<!--conteaza ordinea-->
<asp:Literal id="literalDocumentKeyPressed" runat="server"></asp:Literal>
<asp:Literal id="literal" runat="server"></asp:Literal>

    
</asp:Content>
