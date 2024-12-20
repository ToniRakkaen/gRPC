import React, { useEffect, useState } from 'react';
import { Login_Request } from './proto/MobileGateway_pb';  // Import request message
import { MobileGatewayClient } from './proto/MobileGateway_grpc_web_pb'; // Import the gRPC client

const client = new MobileGatewayClient('http://localhost:8080', null, null);  // URL của gRPC server

const App = () => {
  const [loginInfor, setLoginInfo] = useState(null);
  const [error, setError] = useState(null);
  useEffect(() => {
    // Tạo request
    const request = new Login_Request();
    request.setUserName("minhkhangg32");
    request.setPassWord("Panda_1005");
    request.setClientId("m300.lzl3yhzrksOZqzNBXjZQhsN4QaQIMw");
    request.setSecurityCode("Z6LNilucKdHwXG8QLxZEPI4tAoC5Rj");
    // Gọi API gRPC
    client.Login(request, {}, (err, response) => {
      if (err) {
        setError(err.message); // Set error state
        console.error('Error:', err);
      } else {
        // Chuyển đổi response thành object JavaScript
        if (response && typeof response.toObject === 'function') {
          setLoginInfo(response.toObject());
        } else {
          setError("Response is not in the expected format.");
        }
      }
    });
  }, []);

  return (
    <div>
      <h1>Login Information</h1>
      {error && <p>Error: {error}</p>}
      {loginInfor ? (
        <pre>{JSON.stringify(loginInfor, null, 2)}</pre>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default App;
