<?php

include 'logging.php';

class ReadFile{
  public function __tostring(){
    return file_get_contents($this->filename);
  }
}

class User
{
  public $username;
  public $isAdmin;

  public function PrintData(){
    if($this -> isAdmin){
      echo $this -> username . " is an admin\n";
    } else {
      echo $this -> username . " is not an admin\n";
    }
  }
  
  public function __tostring(){
    if ($this->username != null) {
        return $this.username. " is awesome";
    } else {
        return null;
    }
  }
}

//$obj = new User();
//$obj->username = 'Raju';
//$obj->isAdmin = True;

$obj = unserialize($_POST['data']);
$obj -> PrintData();
//echo serialize($obj);
// for attacking use below
//O:4:"User":2:{s:8:"username";O:8:"ReadFile":1:{s:8:"filename";s:11:"/etc/passwd";}s:7:"isAdmin";b:1;}
?>


