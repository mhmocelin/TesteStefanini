import React, { useEffect } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import { TextField, Button, Paper } from '@mui/material';
import { CreatePerson } from '../types/CreatePerson';
import { UpdatePerson } from '../types/UpdatePerson';
import { Person } from '../types/Person';
import { createPerson, updatePerson } from '../api/personApi';

interface PersonFormProps {
  selectedPerson?: Person;
  onSaved: () => void;
}

const PersonForm: React.FC<PersonFormProps> = ({ selectedPerson, onSaved }) => {
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
    },
    validationSchema: Yup.object({
      name: Yup.string().required('O nome é obrigatório'),
      email: Yup.string().email('E-mail inválido'),
      birthDate: Yup.date().required('A data de nascimento é obrigatória'),
      cpf: Yup.string()
        .matches(/^\d{11}$/, 'O CPF deve conter 11 dígitos')
        .required('O CPF é obrigatório'),
    }),
    onSubmit: async (values) => {
      if (isEdit && selectedPerson) {
        const updated: UpdatePerson = {
          name: values.name,
          email: values.email,
          gender: values.gender,
          birthDate: values.birthDate,
          placeOfBirth: values.placeOfBirth,
          nationality: values.nationality,
        };
        await updatePerson(selectedPerson.id, updated);
      } else {
        const created: CreatePerson = {
          name: values.name,
          email: values.email,
          gender: values.gender,
          birthDate: values.birthDate,
          placeOfBirth: values.placeOfBirth,
          nationality: values.nationality,
          cpf: values.cpf,
        };
        await createPerson(created);
      }

      onSaved();
      formik.resetForm();
    },
  });

  useEffect(() => {
    if (selectedPerson) {
      formik.setValues({
        name: selectedPerson.name,
        email: selectedPerson.email || '',
        gender: selectedPerson.gender || '',
        birthDate: selectedPerson.birthDate.split('T')[0],
        placeOfBirth: selectedPerson.placeOfBirth || '',
        nationality: selectedPerson.nationality || '',
        cpf: selectedPerson.cpf,
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
          label="Nationality"
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
        <Button
          type="submit"
          variant="contained"
          color="primary"
          style={{ marginTop: 16 }}
        >
          {isEdit ? 'Atualizar' : 'criar'}
        </Button>
      </form>
    </Paper>
  );
};

export default PersonForm;

