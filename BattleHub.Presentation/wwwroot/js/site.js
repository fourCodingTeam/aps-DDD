// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function(){
  const body = document.body;
  const btn = document.getElementById('bh-burger');
  if(btn){
    btn.addEventListener('click', ()=>{
      body.classList.toggle('sidebar-open');
    });
  }
  // Close sidebar when clicking a nav link (mobile)
  document.querySelectorAll('.bh-nav-link').forEach(el=>{
    el.addEventListener('click', ()=> body.classList.remove('sidebar-open'));
  });
  // Close when clicking outside overlay
  document.addEventListener('click', (e)=>{
    if(!body.classList.contains('sidebar-open')) return;
    const sidebar = document.querySelector('.bh-sidebar');
    if(!sidebar.contains(e.target) && e.target !== btn && !btn.contains(e.target)){
      body.classList.remove('sidebar-open');
    }
  });
})();
