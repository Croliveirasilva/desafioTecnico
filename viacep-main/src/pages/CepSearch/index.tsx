import axios from 'axios';
import { useState } from 'react';
import ResultCard from '../../components/ResultCard';
import './styles.css';

type FormData = {
  cep: string;
};

type Address = {
  logradouro: string;
  localidade: string;
  bairro: string;
  uf: string;
  complemento: string;

  unidade: string;
  ibge: string;
  gia: string;

};

const CepSearch = () => {
  const [address, setAddress] = useState<Address>();
  const [formData, setFormdata] = useState<FormData>({
    cep: ''
  });

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const name = event.target.name;
    const value = event.target.value;

    setFormdata({ ...formData, [name]: value });
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    axios
      .get(`https://viacep.com.br/ws/${formData.cep}/json/`)
      //.get(`http://localhost:58949/api/cep/Search?zipCode=${formData.cep}`)
      .then((response) => {
        setAddress(response.data);
        console.log(response.data);
      })
      .catch((error) => {
        setAddress(undefined);
        console.log(error);
      });
  };

  return (
    <div className="cep-search-container">
      <h1 className="text-primary">Busca CEP</h1>
      <div className="container search-container">
        <form onSubmit={handleSubmit}>
          <div className="form-container">
            <input
              type="text"
              name="cep"
              value={formData.cep}
              className="search-input"
              placeholder="CEP (somente números)"
              onChange={handleChange}
            />
            <button type="submit" className="btn btn-primary search-button">
              Buscar
            </button>
          </div>
        </form>
        {address && (

          <>
            <table className="tabela">
              <tr>
                <td>Logradouro</td>
                <td>Localidade</td>
                <td>Bairro</td>
                <td>UF</td>
                <td>Complemento</td>
                <td>Unidade</td>
                <td>IBGE</td>
                <td>GIA</td>
              </tr>
              <tr>
                <td><ResultCard description={address.logradouro} /></td>
                <td><ResultCard description={address.localidade} /></td>
                <td><ResultCard description={address.bairro} /></td>
                <td><ResultCard description={address.uf} /></td>
                <td><ResultCard description={address.complemento} /></td>
                <td><ResultCard description={address.unidade} /></td>
                <td><ResultCard description={address.ibge} /></td>
                <td><ResultCard description={address.gia} /></td>
              </tr>
            </table>

          </>
        )}
      </div>
    </div>
  );
};

export default CepSearch;
