<?php
$servername = "localhost";
$username = "root";
$password = "1234";
$dbname = "fps_game";

$user_username = $_POST["usernamePost"];
$user_password = $_POST["passwordPost"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_errno) {
    echo "Failde to connect to MySQL : " + $mysqli->connect_error;
    exit();
}

$sql = "select id
        from user
        where username,$user_username";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    echo "already exist";
    return;
}

$sql = "insert into user(username, password)
        values $user_name password($user_password)";
$result = mysqli_query($conn, $sql);

if ($result === true)
    echo "success";
else
    "fail";
?>