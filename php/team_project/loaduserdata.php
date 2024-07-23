<?php
// user_list.php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST['cuid']
$conn = new mysqli ($servername, $username, $password, $dbname);

if($conn -> connect_errno)
{
	echo "Failed to connect to MySQL : " + Smysqli -> connect_error;
	exit();
}

$sql = "SELECT	lv, exp
        FROM 	character_list
		WHERE	character_uid = $cuid";
$result = mysqli_query($conn, $sql);

if(mysqli_num_rows($result) > 0)
{
	$data = array();
	while ($row = mysqli_fetch_assoc($result))
	{
		$data[] = array('lv' => $row['lv'], 'exp' => $row['exp']);
	}
	
	// $users_json_string 라는 변수 선언 
	// $users 배열을 json_encode() 함수로 json 파일로 만들고 변수에 저장
	$data_json = json_encode($data);
	echo $data_json;
}
else
{
	echo "no user";
}
?>