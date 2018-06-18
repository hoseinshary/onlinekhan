// import $ from "jquery";

// export default {
//   postData(url, data, callback) {
//     if (typeof data == "object") {
//       data = JSON.stringify(data);
//     }

//     $.ajax({
//       type: "post",
//       url: url,
//       data: data,
//       cache: false,
//       //async: false,
//       processData: false,
//       contentType: false,
//       success: function(data) {
//         if (callback) callback(data);
//       },
//       error: function(jqXHR, textStatus, errorThrown) {
//         // showNotifyErr();
//       }
//     });
//   },

//   getData(url, callback) {
//     $.ajax({
//       type: "get",
//       url: url,
//       success: function(data, textStatus, jqXHR) {
//         if (callback) {
//           callback(data, textStatus, jqXHR);
//         }
//       },
//       error: function(jqXHR, textStatus, errorThrown) {
//         // showNotifyErr();
//       }
//     });
//   }
// };
