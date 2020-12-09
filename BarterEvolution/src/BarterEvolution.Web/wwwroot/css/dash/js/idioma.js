<script type="text/javascript">
  var arrLang ={
    'en':{
   "cliente":"clerk",
   "contratos":"contracts",
   "inventario":"stock",
   "ventas":"sales",
   "usuarios":"users",
   "reportes":"reports"
    },
    'es':{
   "cliente":"cliente",
   "contratos":"contratos",
   "inventario":"inventario",
   "ventas":"ventas",
   "usuarios":"usuarios",
   "reportes":"reportes"
    }
  };
  $(function(){
    $('.translate').click(function(){
      var lang = $(this).attr('id');
      $('.lang').each(function(formcliente,element){
        $(this).text(arrLang[lang][$(this).attr('key')]);
      });
    });
  });
 </script>