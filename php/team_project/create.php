<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$userid = $_POST["userid"];
$userpw = $_POST["userpw"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

if (search($conn, $userid) === false) {
    $sql = "INSERT into user(userid, userpw)
            values ('$userid', password('$userpw'))";
    $result = mysqli_query($conn, $sql);

    if ($result)
        echo "y";
} else
    echo "n";

mysqli_close($conn);

function search($conn, $userid)
{
    $sql = "SELECT  userid
            from    user
            where   userid = '$userid'";
    $result = mysqli_query($conn, $sql);
    $count = mysqli_num_rows($result);

    if ($count == 0)
        return false;
    else
        return true;
}
?>