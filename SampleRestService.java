import org.apache.struts2.convention.annotation.Namespace;
import org.apache.struts2.convention.annotation.Result;
import org.apache.struts2.convention.annotation.Results;
import com.opensymphony.xwork2.ActionSupport;

@Namespace("/rest")
@Results({
    @Result(name="success", type="json", params={"root", "message"})
})
public class SampleRestService extends ActionSupport implements Action {

    private String message;

    public String execute() {
        message = "Hello, World!";
        return SUCCESS;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
