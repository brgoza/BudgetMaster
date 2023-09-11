function formatHouseholdUserRows(userData)
{
    var rowsString = "";
    userData.toArray().forEach((value, index, array) =>
    {
        rowsString += "<tr><td>" + value.userName + "</td><td>" + value.email + "</td><td>" + value.phoneNumber + "</td></tr>";
    });
}

function updateNotificationsBadge()
{
    var link = document.getElementById('notifications-link');
    var badge = new bootstrap.Badge();
    var count = fetch('/Data/GetUserNotificationsCount');
    badge.text = count;
    if (count > 0)
    {
        link.firstChild = badge;
    }
    else
    {
        link.removeChild(link.firstChild);
    }
}