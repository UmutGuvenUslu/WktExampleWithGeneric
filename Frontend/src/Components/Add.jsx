function Add(){
    return(
        <>
        <div className=" shadow p-5">
            <svg className="my-2"   style={{ transform: "translateY(-15px)" }} 
 xmlns="http://www.w3.org/2000/svg" width="60" height="60" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
  <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
  <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
</svg>
            <br/>
    <form>
  <div class="form-group">
    <label for="exampleInputEmail1">Şehir Adı Giriniz</label>
    <br/>
    <input type="text" class="form-control my-2" id="Name"  placeholder="Şehir Adı"/>
  </div>
  <br/>
  <div class="form-group">
    <label for="exampleInputPassword1">WKT Giriniz</label>
    <br/>
    <input type="text" class="form-control my-2" id="Wkt" placeholder="WKT Giriniz"/>
  </div>
  <br/>
  <button type="submit" class="btn btn-success px-5">Ekle</button>
</form>
</div>
        </>
    )
}

export default Add