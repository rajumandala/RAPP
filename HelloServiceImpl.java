import javax.jws.WebService;

@WebService(endpointInterface="com.example.helloservice.HelloService")
public class HelloServiceImpl implements HelloService {

    public String sayHello(String name) {
        return "Hello, " + name + "!";
    }
}
