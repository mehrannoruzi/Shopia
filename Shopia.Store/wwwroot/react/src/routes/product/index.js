import React from 'react';
import { Redirect } from "react-router-dom";
import { Card, Button, Carousel, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import productApi from './../../api/productApi';
import Loader from './../../shared/Loader';
import strings from './../../shared/constant';

class Product extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            notFound: false,
            product: {
                name: null,
                price: null,
                likeCount: null,
                slides: [{ imgUrl: 'data:image/gif;base64,R0lGODlhIAAgALMPAPj4+Pf39/X19fT09Pb29vPz8/39/fLy8vn5+fr6+vHx8fv7+/Dw8Pz8/O/v7+/v7yH/C05FVFNDQVBFMi4wAwEAAAAh+QQFBAAPACwAAAAAIAAgAAAEItDJSau9OOvNu/9gKI5kaZ5oqq5s675wLM90bd94ru+8HAEAIfkEBQIADwAsAAAAAAEAAgAABAOQsQgAIfkEBQIADwAsAAAAAAMABwAABAuwKHYYmw+z93bnEQAh+QQFAgAPACwAAAAABQANAAAEHFCMo5goh7FR6psexjxPNz7UmZGPR7rPScox+0QAIfkEBQIADwAsAAAAAAcAEgAABC0QBDKOYoCIchimxfUEQiWSHPM8oPiUlvqG8mPW9/rQ+hP3P51LWFsVjT1kMgIAIfkEBQIADwAsAAAAAAgAFQAABDswoUDGUQwBIsphGTUUmDMJVrl1n+OIJOMG6CU7Vezi6e2wJVcn6OrtHB4iUumwHZu+HdMxje6sLqwjAgAh+QQFAgAPACwAAAAACgAbAAAEV7CthAIZRzGJABHFwTBTdRXaMwGBgKVL94XM81DWgNY362Y8mS5lq/yID18I6RnybK3X89FaTk9I23H6AIls4IczbJOSH7QzOgsGqr9qNlhu44btYLwtAgAh+QQFAgAPACwAAAAADAAgAAAEdtCYthIKZBzFJkUAIRQH01EWNhTcM1VAIGgtCook8zy2yuo8mIwGbFhCq9aucpltgI8FSEZSRi+Z326XiDmtjy7uuX1gk9Bdk1h+hEaltjsL3lHJ7WxcnsG34XU7I4E7bHIPhnJahw9+cnuMhFuSO2mHlnKYbREAIfkEBQIADwAsAAAAAA4AIAAABIqwNWPaSiiQcRSTlYUAhFAczEdZmDYUnjNJFxAIXLxeY3kyDseutYEBhbSEDdc5VnikVyz4bDGnyMXodsKyMkWsrHbLHYMikqkZDPJcxrZbWWbLteqfPEiUntt0a2JBPS8oe4QudntLXX9tUXGIDnWDbVyLe2GPclecbWufbX6To5mIeqVBkqqniBEAIfkEBQIADwAsAAAAABAAIAAABKAQrdaMaSuhQMZRTDJV1IIAhFAcTDhZmMYNBeiMVwwEgmfjsVNqxXA4KLDMplMrHkk6ns+JDKJoNiNUKf04HTDMibfKgi9cphlcSux6XqMxZ0Kp4nK0TP2dR+FrTxp2RHJyQTNNhloZb2V9WoNMLItGaVOVN2N3gZZLWJBybl2dRm5DeJWfipkOG4ChcoSUrQ5XrK2ksXKou7yYtQ6cvkYRACH5BAUCAA8ALAAAAAASACAAAAS0kIC0WjOmrYQCGYfCBFP1ZBoCEEJxMAyAUFe2dV8hPrKJboCAALSb+TScVev1eBhrSNxAx2jSThagkFh9XG3J3K65WGCj21D3cUwFl2M29OaZxh+Ns3aobjbzPyosLndzHHVUfn4/CW9ciicoYUtri2BSiZCMb4SVTZcrU0yQWHQffaQ2KkKdpHimdp5+SI6opG6DtpANh2KyfnuPrmyClMNWmHekjWnKkMUuv4pSuq6c1aQRACH5BAUCAA8ALAAAAAAUACAAAATKcAgC0mrNmLYSCsRwKIwUVFeGLQhACMXBlESAWNnWfWFBOhMAIrXhJAABgehXQ2F0HhdM5nBQbheNkTfwMaqn4XN1TC6/DhtOtXN1f1Uhrrgzj9AOp4rTSsbgDlg5WyBveIFEZEhKd1VVa3QtL3+Hc1BcXo5ViUaLZ5oOnFGTVKBPl4WZpnsdi5SgDmNtPaWmWnUhjbBafK66oLceqYDAinbEmpFSr7AOqD3IyZ3Hh6ssy7XNhNDVpq3UzY4No1PdoLif4Zt9U9GgEQAh+QQFAgAPACwAAAAAFgAgAAAE5VCdIghIqzVj2kpIQAyHwkiDEFzZpi0IQAjFwZzFQAQItnWf0KhgckwqAESr40kAAgJSMadiaYAgGc3mcOQsvQynKRwQGd0UePlyQqVoR4rncwVl5mIXGXaR3yVxDlV1TDBPW3oOO31jQSJ5gg4rSldtiHBdXSuFLzEzNYoOST6OIJBnml1JbE2YgaoOfX5ZoFyxjVhlqbGdrlChkl2dd0O3sQtiupCwsQ6th8DNyD9/Q6Kqlr9R07Hah7bYmtWP18LZhm7c4ppjHp9b56qmu+ztl4D2XbpaNfLz1jI5Axgt0T9NEQAAIfkEBQIADwAsAAAAABcAIAAABPOQqVMEAWm99kxbCRIQw6Ew0jEIAaY1xrYgACEUx4MqxUAEiAzHAxKRCqfHpHJBvGKfBCAgKCUnq1ZmQwzVbgfG40HxAYKLYdQ4QIrJzPNTJqVa3z0WmruutZNjcWgwdCJVJm8PWS5cMjRUOICKP4MdayN/iQ8Wco1RU4eSm1pzHzQ2kZqUQpZFmG5jsS1OniCgd7FjpJ5eqGG5Y2esXWywwJ2En5CIwA97MH1Hv82VxJjMzVvJM6CpzQ/Dl0eiuaW2dtjN5qdg5LFprSGv7rnbdaGawA3b7Dj5+vGK0csF5Ry+bwSjgfn3TRwuhPW4LRsYKwIAIfkEBQIADwAsAAAAABkAIAAABP/QSXWKICCt1oxpS4IExHAojMRQgxBkW8ctCEAIxcGkzloMhABC0/mERqUCSkWxYBCxDygBCAhMyx7LBeMYRTacTqL9PYmeKXKg5K0qFwDUO6Nase6tkCg72thZPXBnG1JHdyc8Dk1cfIY1VjmBCmZ7hV9rbWQOg3JRdVVXiZsHLS+OMzU3kooOlUOXaiSAra5xczJToXibDqZduWCrY70XlkWySYFkt5+6kaO9QbB0fknE0nJomLPRvafONKGsvRLasYfKtWTadM+iy5vUueLD8WTUaem05WSo77z6OfhXT8y6TQvQiZilSaCDcHbgHSTzyZpBhxLc7fMmUCMkMfcDekUAACH5BAUCAA8ALAAAAAAbACAAAAT/0EmpThEEpNWaMc2SIAExHAozOUw1CIHGed2CAIRQHIw6tYUBIYDYeEAikqmQWrUsGABiBgolAAHBqflzwWQd5Ain461YimCmyPlYlQOmT/K8rKm1a3Y7Rx9eREY0SThxXD9Qa0ZVSXsofXVfgow3WTuHaGqBbWJwck4MiVJ4Vlhaj05eMZM1NzmXkGlCm0dvJYZ9DhV2o2F5pnxnqmA0DWOvZmcWs2y1jUuYEqJTvqWWqGdBMM2Dx3HJ2UJSi5232GeAxJSmsGcSzOS20LkTUUXVIsDnZ5KkNmTt3DkY0szNM1wCJcSgViyfo2grVvlzVYbeinGc5CFM6KBXQz2nFiCu4NYJILiEBfPA2SeQVYhKZUSuiAAAIfkEBQIADwAsAAAAAB0AIAAABP/QyenUKYKAtFozRrMkSEAMh8JQEmMNQrB1n7cgACEUB7OyrsKAEEBwPqFR6VRQsRyuSwaAoIVECUBAgHICX7GZJ0nK7XpPqEKoMXZA2OWg+aNEMW2rLbvt1idRMEVHNUo5c15AUm1HV0p9KX8tFhgyhI44WzyJgGtDg29kcnRpDItUelhaXJFPgWGXNjg6m5JqbKBIcSaItndTVWN7q36uYJapZbRoxrhuuo9MnJOnwTWqmq1fB4LPhcpzzIpsVI2ivNoslLCpN6u1aZ5Ez3DRvWkO1QnC2KzTE9zY8XO37J8EZ+Z2SbM1AQ+qgXz8MZQgSMw1gmcmSsiQqx4JXqQe8DkA1i5iMZHzYhlaplECozd75KTDh2xgpjMGKUQAACH5BAUCAA8ALAAAAAAfACAAAAT/0Mk51SmCgLRaM0azJEhADIfCUBRjDUKwdZ+3IAAhFAezshJXYUAIIDif0Kh0KqiAQQtGg6CFRAlAQIB6Ql0HmAxZW+Z2PWh0SEWCsMyB8wcETwFVz3Wk5aboLGBiR1ZwOXJeXxcZeG42JFtdgC1SMTN6NjhbPImBCmxGbkqQTZ2Bi1SFWH2SaoKWZHs4OpyTE0JEoR1vSyaItlGoeKp8kX+uL7CqJGc8wGu5hElwvnPIwnk1q5vHX8m6mGZNaa4XRI27o3HddZVjxJpopref0aLUpc8+2AmY237zgom5pO1GM3aeQEnjReqXGgeVUvkrBvAZxDDKJs5CYxGiwnu9I/I9dICNWBZjAScMiSGtDDNa5B6yQVewIUIoA2Nl6lNrpIMIACH5BAUCAA8ALAAAAAAgACAAAAT/0MlJ1SmCgLRaM0azJEhADIfCUCxjDUKwdZ+3IAAhFAezshNXYUAIIDif0Kh0KqiAQQtGg6CFRAlAQIB6Qh2uA0yGrC1zu95XIiQCjh0Qljlw/qDh6dtqy267d0BhY3AeSiQ5dV5fDBcZb0hXS38pgS1SMTOGNjhbPIuCCkNFhXKTTaCCjlR8WFpclWuDmWWSODqflhRtpJGHdHaymKybrpSpuy+0rYi4asKjRr5zJoq6Uat7xSOvucJiMYVmzXXPjBdupb/VsYyYZK033e14okTSceuo10HZR9t+YCHDNkZTDRG30gxkY69XPmr7ZDXSUwXgK0DfCtbihIYHP4bRJtRBtLbGwTBtB7kd+2gSHL5NZ5yxdICOSqQ+dOh9cQRvWyeFMyMAACH5BAUCAA8ALAAAAAAgACAAAAT/0MlJpTpFEJBWa4bRLAkSEENRrcw1CAHngd+CAIRQHGvVFgNCANEBiUgmVEHRm7QwGgBiJholAAHB4MBsOp6vWJGGxOkODO9XAdwQPSFrMqVIN58ZN7V2zW7rXmAwbx9HJTh0dndQbkVVSH5ciiwXGWJ7IzdZO4B3bEFDjoZzS5MsjFKYJFhakoEuMDKFNTc5nKYUP6CEcZBKnT14UVOzVqx/uE6wl8WHtmhqukK8oyeJr6jENMabrp4HYYRkzinQgRhBUqJy1t7BlbFjj5pnwJRtoXDVv8lO2QnFVkWy52OZrG02zNx6hY8aO37Rsqnq04pgLoPyaCk056nhOl/XH77lSRWQIjJs4TKWedZPgjR1cPjMcUcJCjOE9BY2iQAAIfkEBQIADwAsAAAAACAAIAAABP/QyUmpOkUQkFZrhtEsCRIIVSox1yAEnAd+CwIQqFqxxUAEiA5IRDIRBjoKC6MBIGSiUQJwQiZXrVfsQyzdBIUrtrcJekJS4yB8XWbKUNq0ehAvXUDhrHhb19tMZUJRRXQKbRcZMHqENicFB4dJPD55Z11qBQoMgG9OcVJUAgORnDp3Wow0NjiQm5MKZJZDaQFHmqYqbk1PXHKipK+nWYugXq0HubqxlWa0hbfCy4GfvqGPpbAHeM57x37KKQwYPk6DmLbB4UqJqaA1oq7rE5Q/zmjQa9K61EHWJMCyDdvm7h8rMAKXybqHLto8ev3ezRmVUByxLTNGHIT0cEyzc7UgHGrz1CsjQEMdUc3ytjEZIjLmzshRU3FHu2IG49WUEAEAIfkEBQIADwAsAgAAAB4AIAAABP/QyUmpOkUQkFZrhtEsCVCdEnMNQsB54LcgJlqpxUAESAeKJJqNosJoAAiYaFQKDCdFlssXCwKcTwdOh/SFmIgAIVvMbJKfJekqIK9aPSUYQGg/GcazV5YICwZ3FxlTciM0AX+BOTtxP2BiAwV3eUiFa4gDB0NRLS9pMjR1BZo2W4xeQH2QBQqlgkdoMUxsma0onISfDX10AqMMros8qI8EkQrAJ2Wwlk2+B8i3b8NyVsa/pRhcjV9BkNDJN6+5sjNso9HKCsLcqWHX6cqUPbqXf+DSB1Ke5aHP8UTW6aDmyBu8cDfmNaOFT52+TlTU+MMmjR0xg8cQQnl1ZiGmhuIaHhKswkvUAY0bF3XxoEaVMZABjZCTeA6mgwgAIfkEBQIADwAsBAAAABwAIAAABP/QyUmpOkUQkFZrhtEsVSkx1yAEnAd+pFmhxUAESAeKSyJTKIwGgHCJRr7fKbVqfXgJhHJZ2+Q8IaRUGcxYjbAEYBpU4XSvHmLMFVp1R3WAe8mw0PH1/EeznbFQCAEEbV5EYEgAg3xMd4gLawQCjFV/O1qDAzJdQ0VPYYoCmiZlTXgfUQCSBZsKlVeXcgQDrCWcX5+JAQIFB6SNsGmps72kGDZEcIGZBwq2daaPerzNz6/KmMQKDLZuybk9oQPN3ECNTi8jkdTbM65+sFmytO0z3p7p4bvj9ROljuDW9Spn7hqgbPQILrn3SMw+cu4OmAkWSBU1hVSQxQsjaBZEc0IHAOab1qtfBAAh+QQFAgAPACwGAAAAGgAgAAAE+tDJSak6RRCQVmuGUY0Scw1CwHlgQ45mMRAB0oGG+04mpgEIVk63c/RQqluLuIvNgLdQY1E0XjKb4GdILR5TNuG02/Rlo59FwntNrbbpdVMho4Vx0wTCawaK83svX0l/CwgAc3U1UTlqh4JtWYUJAAGQB0hvS3qVkIp3Uo4BBCQ9WH5weZUCpSdgSo2cBKwwGE+gsQijAzBthKmGq7wVTnaMgLsFtac2wJQBAgMHxK6/S8GzBdMUxYseoXrJCsR9WtfP0QfjPNWaXIfZ6tx0M954ogQDBQoM7OWTwtT1K1HtTgtHAOINJFgHyrc4uvIJ9JfB2rtV2vg5iAAAIfkEBQIADwAsCAAAABgAIAAABPDQyUmnOkUQkFZrRiU6zDUIAeeBY1UWAxEgHRi2UolpALIaN5zulKqBGricAraheYBI4SXT/H2SQ5TzY7gKd81at7GQ7opWspQ5E3cX5RYD3EsvEnITSsX93PNsW1B3CCM6VHV9ZAmFIlloiowAhksxbU9vko5TPD6KdwABmwdEW0eEAASOGDE9bosIAaoUh3tGYwsIoQIulTKCmbEEvLR0NJ8JuwO0epBHZLrDyxMvlsCwsgMFxYiez6ABAgMH1M1839ECBeRKga+E2QUKSsZ2yeHj8yTNpsGp6gcYtGslyA+jbAcUCKzlDJeucOsURgAAIfkEBQIADwAsCQAAABcAIAAABNfQyUmpOkUQkFaroMRcgxBwXgiOxUAESPep04hpAOLNtGOXJ5mhJ1K0NjHPsGfLIHdL2s+UbDSiKsYNKcSuLpkg1FthuWBdXkjrzEGtUpIJZTXAs8Zz1WBf4LdudVZ+a3Jigg2EX0doSn0JhYA6iAuQXwdAVVeJCQhrGC45aZWeZWBzMpuJCACLeqOdrWWSCZQJAAGmmKhvq7imjHuPCAEEs22TqpW4AhRTh8qsBM01eS/CnMQEA9WSvcsBAtxFu42CpADTBdVHokqDncUD6z6n0Ha+4QUHEQAh+QQFAgAPACwLAAAAFQAgAAAEwdDJSak6RRCAqpfMNQgBkHxeWAxEgJzoFGIat8SySJo37qgsTqLhm2U2iAURNxu5EsoiDQldooy7qoGpWLVeym2MMbU1GuJPM6tEj7usZzhdwSLbBjplXaqirXVwX1oGgDllSWeFhiA6copnahhBYGiFPXsXGX14Z5g5Xo95DQufIIhDf6Qwh048kKuBoZWjCwkde4idq7iNB65+iwsIAJmzhLzFvkdmqrYAAa0klX+2xASglFqetwHYP5pssMPQAhEAIfkEBQIADwAsDQAAABMAIAAABKnQyUmpOkWQyiW7gxB03FcMBEBSH6apqweKSCyfaR23WZrYrVAA8du5covdJRMAJJIrE2r4NPYAiEUjOms+tyQpgaoNL1/ZhsF8EDq1646YrJafkY16qUvU6isMGChYXwZxLGdefoZ7OHSGYCxHhHkGeYBdb5WXiI59apaRHpNpanlQMm0imqYNqA5zn5ALRbB3WIuntUE0X6a0OrY4hHC6OjyrvqELCAARACH5BAUCAA8ALA8AAAARACAAAASS0MlJqTqjaslu3lVXfCB3FUI5dRihmqOrsmjwsoNgzwdKALOTDlgSDQgBBM8HUIJww0RREUtKNzTBD7F4eqJdLPVobYh72ubCrIFa19jesZmAh4QBQL1haI+RCHt9IWhbCQ18dxhgiIMrf298jhyFao2TbnprfGwwZIFrBgadWVubo6RfkZypMXShiGFZeXusCxEAIfkEBQIADwAsEQAAAA8AIAAABIDQyUmpqliym+vu1HaA01aQmnKS4oC271EIrCLToDkQ7B3ktoHg19ERAEXb7Jg8CAOIZGEHzYiWgIQ1OERoMQzZDoBYgJXDrNmjonrXodvR22A704lFPdQmQPN7JXJkeoEOLXh6BnxTfm8NiymDZQ2QJVx/hZGHfWSABpFXiZWgEQAh+QQFAgAPACwTAAAADQAgAAAEc9DJSSWr2N1MN9fKZ4Uic5TK+TFKUR4uxx7D2tYyLMjpsGeswoAATBUEgSINWRQSAJjZ8RntIRFR2BCA7RiRgEQlOAwgxB3dE7HwLgPh9oRMMCfkFjV30Zhb4Xd9I052fCN6bA0Ghz6FioNbZ3yLUmCBihEAIfkEBQIADwAsFAAAAAwAIAAABFvQyUmrvTjrzSvbnxZijCKaGXOkykoqxXvEV3kMbyHUrc7fhNpsEPT0BAFLqUAEKGcCgpNiGyARRiYhgKVCpQlqyxoAhCdLIneBPkoRbEn1mog70u9EQ34s1xsRACH5BAUCAA8ALBYABgAKABoAAAQ/0MlJq704a8pyx5/FKCB5MceppKJSnMdbjcfQFvYcC/M68BzXgKArCALB2jGIIwAmNKMT6jsiqE3A1RE9AhIRACH5BAUCAA8ALBgACwAIABUAAAQp0MlJq704a3a59RSjfGPFHKaChkphHu4kHgNbCLJ65zSRFwOfY3YLRAAAIfkEBQIADwAsGgARAAYADwAABBjQyUmrvZdVTflUHTgxokMeo1Kkq6kcQwQAIfkEBQIADwAsHAAWAAQACgAABArQyUmrtWxmuZmKACH5BAVPAA8ALB4AHAACAAQAAAQE0MkpIwA7' }],
                desc: null
            }
        };
    }

    async componentDidMount() {
        const { params } = this.props.match;
        this.setState(p => ({ ...p, loading: true }));
        let product = await productApi.getProduct(params.id);
        this.setState(p => ({ ...p, product: { ...product }, loading: false }));
        //let comp = this;
        //after api call
        // setTimeout(function () {
        //     comp.setState(p => ({ ...p, notFound: true }))
        // }, 2000)
    }
    _purchase() {

    }

    render() {
        let p = this.state.product;
        if (this.state.notFound)
            return (<Redirect to="/notFound" />);
        return (
            <div>
                <Carousel>
                    {this.state.product.slides.map(s => (
                        <Carousel.Item>
                            <img className="d-block w-100" src={s.imgUrl} alt="slide" />
                            <Carousel.Caption>
                                <h3>{s.Title}</h3>
                                <p>{s.Desc}</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    ))}
                </Carousel>
                <Container>
                    <Row className="icons">
                        <Col xs={4} sm={4}>
                            <button className="btn-favorite">
                                <svg aria-label="Save" class="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path d="M43.5 48c-.4 0-.8-.2-1.1-.4L24 29 5.6 47.6c-.4.4-1.1.6-1.6.3-.6-.2-1-.8-1-1.4v-45C3 .7 3.7 0 4.5 0h39c.8 0 1.5.7 1.5 1.5v45c0 .6-.4 1.2-.9 1.4-.2.1-.4.1-.6.1zM24 26c.8 0 1.6.3 2.2.9l15.8 16V3H6v39.9l15.8-16c.6-.6 1.4-.9 2.2-.9z"></path></svg>
                            </button>
                        </Col>

                        <Col xs={8} sm={8} className="col-2">
                            {/* <button className="btn-share">
                            <svg aria-label="Share Post" class="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path d="M46.5 3.5h-45C.6 3.5.2 4.6.8 5.2l16 15.8 5.5 22.8c.2.9 1.4 1 1.8.3L47.4 5c.4-.7-.1-1.5-.9-1.5zm-40.1 3h33.5L19.1 18c-.4.2-.9.1-1.2-.2L6.4 6.5zm17.7 31.8l-4-16.6c-.1-.4.1-.9.5-1.1L41.5 9 24.1 38.3z"></path><path d="M14.7 48.4l2.9-.7"></path></svg>
                        </button> */}
                            <button className="btn-comment">
                                <svg aria-label="Comment" class="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path clip-rule="evenodd" d="M47.5 46.1l-2.8-11c1.8-3.3 2.8-7.1 2.8-11.1C47.5 11 37 .5 24 .5S.5 11 .5 24 11 47.5 24 47.5c4 0 7.8-1 11.1-2.8l11 2.8c.8.2 1.6-.6 1.4-1.4zm-3-22.1c0 4-1 7-2.6 10-.2.4-.3.9-.2 1.4l2.1 8.4-8.3-2.1c-.5-.1-1-.1-1.4.2-1.8 1-5.2 2.6-10 2.6-11.4 0-20.6-9.2-20.6-20.5S12.7 3.5 24 3.5 44.5 12.7 44.5 24z" fill-rule="evenodd"></path></svg>
                            </button>
                            <button className='btn-like'>
                                <svg aria-label="Like" class="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path clip-rule="evenodd" d="M34.3 3.5C27.2 3.5 24 8.8 24 8.8s-3.2-5.3-10.3-5.3C6.4 3.5.5 9.9.5 17.8s6.1 12.4 12.2 17.8c9.2 8.2 9.8 8.9 11.3 8.9s2.1-.7 11.3-8.9c6.2-5.5 12.2-10 12.2-17.8 0-7.9-5.9-14.3-13.2-14.3zm-1 29.8c-5.4 4.8-8.3 7.5-9.3 8.1-1-.7-4.6-3.9-9.3-8.1-5.5-4.9-11.2-9-11.2-15.6 0-6.2 4.6-11.3 10.2-11.3 4.1 0 6.3 2 7.9 4.2 3.6 5.1 1.2 5.1 4.8 0 1.6-2.2 3.8-4.2 7.9-4.2 5.6 0 10.2 5.1 10.2 11.3 0 6.7-5.7 10.8-11.2 15.6z" fill-rule="evenodd"></path></svg>
                            </button>
                        </Col>
                    </Row>
                    <Row className="text-box">
                        <Col>
                            <span className="title">
                                {this.state.product.name}
                            </span>
                            <p>
                                {this.state.product.desc}
                            </p>
                        </Col>
                    </Row>
                </Container>

                <button className="btn-purchase" onClick={this._purchase.bind(this)}>{strings.purchase}</button>
                <Loader show={this.state.loading} />
            </div>
        );
    }
}
// const mapStateToProps = state => {
//     return { ...state.homeReducer };
// }

// const mapDispatchToProps = dispatch => ({
// });

export default connect(null, null)(Product);
