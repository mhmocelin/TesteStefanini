import React, { useEffect } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import { TextField, Button, Paper } from '@mui/material';
import { CreatePersonV2 } from '../../types/V2/CreatePersonV2';
import { PersonV2 } from '../../types/V2/PersonV2';
import { createPersonV2, updatePersonV2 } from '../../services/personServiceV2';

interface PersonFormV2Props {
  selectedPerson?: PersonV2;
  onSaved: () => void;
}

const PersonFormV2: React.FC<PersonFormV2Props> = ({ selectedPerson, onSaved }) => {
  const isEdit = Boolean(selectedPerson);

  const formik = useFormik({
    initialValues: {
      name: '',
      email: '',
      gender: '',
      birthDate: '',
      placeOfBirth: '',
      nationality: '',
      cpf: '',
      address: {
        street: '',
        number: '',
        neighborhood: '',
        city: '',
        state: '',
        country: '',
      },
    },
    validationSchema: Yup.object({
      name: Yup.string().required('O nome é obrigatório'),
      email: Yup.string().email('E-mail inválido'),
      birthDate: Yup.date().required('A data de nascimento é obrigatória'),
      cpf: Yup.string()
        .matches(/^\d{11}$/, 'O CPF deve conter 11 dígitos')
        .required('O CPF é obrigatório'),
      address: Yup.object({
        street: Yup.string().required('Rua é obrigatória'),
        number: Yup.string().required('Número é obrigatório'),
        neighborhood: Yup.string().required('Bairro é obrigatório'),
        city: Yup.string().required('Cidade é obrigatória'),
        state: Yup.string().required('Estado é obrigatório'),
        country: Yup.string().required('País é obrigatório'),
      }),
    }),
    onSubmit: async (values) => {
      if (isEdit && selectedPerson) {
        const updated: Omit<CreatePersonV2, 'cpf'> = {
          name: values.name,
          email: values.email || null,
          gender: values.gender || null,
          birthDate: values.birthDate,
          placeOfBirth: values.placeOfBirth || null,
          nationality: values.nationality || null,
          address: values.address,
        };
        await updatePersonV2(selectedPerson.id, updated);
      } else {
        const created: CreatePersonV2 = {
          name: values.name,
          email: values.email || null,
          gender: values.gender || null,
          birthDate: values.birthDate,
          placeOfBirth: values.placeOfBirth || null,
          nationality: values.nationality || null,
          cpf: values.cpf,
          address: values.address,
        };
        await createPersonV2(created);
      }

      onSaved();
      formik.resetForm();
    },
  });

  useEffect(() => {
    if (selectedPerson) {
      formik.setValues({
        name: selectedPerson.name,
        email: selectedPerson.email ?? '',
        gender: selectedPerson.gender ?? '',
        birthDate: selectedPerson.birthDate.split('T')[0],
        placeOfBirth: selectedPerson.placeOfBirth ?? '',
        nationality: selectedPerson.nationality ?? '',
        cpf: selectedPerson.cpf,
        address: {
          street: selectedPerson.address.street,
          number: selectedPerson.address.number,
          neighborhood: selectedPerson.address.neighborhood,
          city: selectedPerson.address.city,
          state: selectedPerson.address.state,
          country: selectedPerson.address.country,
        },
      });
    }
  }, [selectedPerson]);

  return (
    <Paper style={{ padding: 16, marginBottom: 16 }}>
      <form onSubmit={formik.handleSubmit}>
        <TextField
          fullWidth
          label="Nome"
          name="name"
          value={formik.values.name}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.name && Boolean(formik.errors.name)}
          helperText={formik.touched.name && formik.errors.name}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Email"
          name="email"
          value={formik.values.email}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.email && Boolean(formik.errors.email)}
          helperText={formik.touched.email && formik.errors.email}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Gênero"
          name="gender"
          value={formik.values.gender}
          onChange={formik.handleChange}
          margin="normal"
        />
        <TextField
          fullWidth
          type="date"
          label="Data de Nascimento"
          name="birthDate"
          value={formik.values.birthDate}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.birthDate && Boolean(formik.errors.birthDate)}
          helperText={formik.touched.birthDate && formik.errors.birthDate}
          margin="normal"
          InputLabelProps={{ shrink: true }}
        />
        <TextField
          fullWidth
          label="Local de Nascimento"
          name="placeOfBirth"
          value={formik.values.placeOfBirth}
          onChange={formik.handleChange}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Nacionalidade"
          name="nationality"
          value={formik.values.nationality}
          onChange={formik.handleChange}
          margin="normal"
        />
        {!isEdit && (
          <TextField
            fullWidth
            label="CPF"
            name="cpf"
            value={formik.values.cpf}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.cpf && Boolean(formik.errors.cpf)}
            helperText={formik.touched.cpf && formik.errors.cpf}
            margin="normal"
          />
        )}

        {/* Campos de endereço */}
        <TextField
          fullWidth
          label="Rua"
          name="address.street"
          value={formik.values.address.street}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.address?.street && Boolean(formik.errors.address?.street)}
          helperText={formik.touched.address?.street && formik.errors.address?.street}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Número"
          name="address.number"
          value={formik.values.address.number}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.address?.number && Boolean(formik.errors.address?.number)}
          helperText={formik.touched.address?.number && formik.errors.address?.number}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Bairro"
          name="address.neighborhood"
          value={formik.values.address.neighborhood}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.address?.neighborhood && Boolean(formik.errors.address?.neighborhood)}
          helperText={formik.touched.address?.neighborhood && formik.errors.address?.neighborhood}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Cidade"
          name="address.city"
          value={formik.values.address.city}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.address?.city && Boolean(formik.errors.address?.city)}
          helperText={formik.touched.address?.city && formik.errors.address?.city}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Estado"
          name="address.state"
          value={formik.values.address.state}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.address?.state && Boolean(formik.errors.address?.state)}
          helperText={formik.touched.address?.state && formik.errors.address?.state}
          margin="normal"
        />
        <TextField
          fullWidth
          label="País"
          name="address.country"
          value={formik.values.address.country}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.address?.country && Boolean(formik.errors.address?.country)}
          helperText={formik.touched.address?.country && formik.errors.address?.country}
          margin="normal"
        />

        <Button
          type="submit"
          variant="contained"
          color="primary"
          style={{ marginTop: 16 }}
        >
          {isEdit ? 'Atualizar' : 'Criar'}
        </Button>
      </form>
    </Paper>
  );
};

export default PersonFormV2;
