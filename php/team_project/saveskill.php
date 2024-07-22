<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$num = $_POST["num"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "UPDATE 	character_list
		SET		skill = $num
		WHERE 	character_uid = $cuid";
		
$result = mysqli_query($conn, $sql);

$sql = "SELECT	skill
		FROM	character_list
		WHERE	character_uid = $cuid";
		
$result = mysqli_query($conn, $sql);
$row = mysqli_fetch_assoc($result);
echo $row['skill'];

return;
?>