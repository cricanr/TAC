

function getViewportDimensions()
{
 var heightValue = 0;
 widthValue = 0;

 if ( window.innerHeight )
 {
  heightValue = window.innerHeight;
  widthValue = window.innerWidth;
 }
 else if ( (document.documentElement) && (document.documentElement.clientHeight) )
 {
  heightValue = document.documentElement.clientHeight;
  widthValue = document.documentElement.clientWidth;
 }
 else if ( (document.body) && (document.body.clientHeight) )
 {
  heightValue = document.body.clientHeight;
  widthValue = document.body.clientWidth;
 }

 return { Height: parseInt(heightValue, 10), Width: parseInt(widthValue, 10) };
}
