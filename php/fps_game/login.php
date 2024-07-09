<?php
// login.php
$servername = "localhost";
$username = "root";
$password = "1234";
$dbname = "fps_game";

$user_username = $_POST["usernamePost"];
$user_password = $_POST["passwordPost"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
	exit();
}

$sql = "select password
		from user
		where username = '" . $user_username . "'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
	if ($row = mysqli_fetch_assoc($result)) {
		if ($row['password'] == get_enc_str($conn, $user_password))
			echo "login success";
		else
			echo "password incorrect";
	}
} else
	echo "user not found";

return;

function get_enc_str($conn, $str)
{
	$sql = "select password('" . $str . "') as 'enc_str'";
	$result = mysqli_query($conn, $sql);
	$row = mysqli_fetch_assoc($result);

	return $row['enc_str'];
}
?>