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

$sql = "SELECT  userpw
        from    user
        where   userid = '$userid'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    if ($row = mysqli_fetch_assoc($result)) {
        if ($row['userpw'] == get_enc_str($conn, $userpw))
            echo "success";
        else
            echo "incorrect";
    }
} else
    echo "incorrect";

mysqli_close($conn);

function get_enc_str($conn, $str)
{
    $sql = "select password('$str') as enc_str";
    $result = mysqli_query($conn, $sql);
    $row = mysqli_fetch_assoc($result);

    return $row["enc_str"];
}
?>