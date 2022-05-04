/*
 * Created by SharpDevelop.
 * User: Abel Agustian
 * Date: 24/04/2022
 * Time: 14:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace Tugas1Lab
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		MySqlConnection co = new MySqlConnection("Server=localhost;Database=pesertaUTBK; Uid=root");
		MySqlCommand mycommand = new MySqlCommand();
		MySqlDataAdapter myadapter = new MySqlDataAdapter();
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			co.Open();
			ReadData();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public void ReadData(){
			try{
				mycommand.Connection = co;
				myadapter.SelectCommand = mycommand;
				mycommand.CommandText = "select * from pesertaUTBK";
				DataSet ds = new DataSet();
				
				if (myadapter.Fill(ds,"dftpesan")>0){
					dataGridView1.DataSource = ds;
					dataGridView1.DataMember = "dftpesan";
				}
			}
			catch (Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
		
		public void InsertData(){
			try{
				mycommand.Connection=co;
				mycommand.CommandText="insert into pesertaUTBK values('"+nomor_peserta.Text+"','"+nama_peserta.Text+"','"+tanggal_lahir.Text+"','"+nisn.Text+"','"+universitas1.Text+"','"+pil1.Text+"','"+universitas2.Text+"','"+pil2.Text+"','"+rataTka.Text+"','"+rataTps.Text+"','"+nilaiUtbk.Text+"')";
				myadapter.SelectCommand= mycommand;
				if (mycommand.ExecuteNonQuery()==1){
					MessageBox.Show("Data berhasil dimasukan","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
					ReadData();
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
		public void UpdateData(){
			try{
				mycommand.CommandText = "update pesertaUTBK set nomor_peserta='"+nomor_peserta.Text+"',nama_peserta='"+nama_peserta.Text+"',tanggal_lahir ='"+tanggal_lahir.Text+"',nisn ='"+nisn.Text+"',universitas1 ='"+universitas1.Text+"',pil1='"+pil1.Text+"',universitas2 ='"+universitas2.Text+"',pil2 ='"+pil2.Text+"',rataTka = '"+rataTka.Text+"',rataTps='"+rataTps.Text+"',nilaiUtbk ='"+nilaiUtbk.Text+"' where nomor_peserta ='"+nomor_peserta.Text+"'";
				myadapter.SelectCommand = mycommand;
				if (mycommand.ExecuteNonQuery()==1){
					MessageBox.Show("Data berhasil diupdate","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
					ReadData();
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
		
		public void DeleteData(){
			try{
				mycommand.CommandText="delete from pesertaUTBK where nomor_peserta='"+nomor_peserta.Text+"'";
				myadapter.SelectCommand = mycommand;
				if (mycommand.ExecuteNonQuery()==1){
					MessageBox.Show("Data berhasil dihapus","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
					ReadData();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		
		
		public void CariData(){
			try{
				myadapter.SelectCommand = mycommand;
				mycommand.CommandText = "Select * from pesertaUTBK where nomor_peserta like '%"+textBox1.Text+"%' or nama_peserta like '%"+textBox1.Text+"%'";
				DataSet ds = new DataSet();
				
				if (myadapter.Fill(ds,"dftpesan")>0){
					dataGridView1.DataSource = ds;
					dataGridView1.DataMember = "dftpesan";
				}
			}
			catch (Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
		
		
		void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			nomor_peserta.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			nama_peserta.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
			tanggal_lahir.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
			nisn.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
			universitas1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
			pil1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
			universitas2.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
			pil2.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
			rataTka.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
			rataTps.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
			nilaiUtbk.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
		}
		
		void HitungNilaiClick(object sender, EventArgs e)
		{
			//TKA
			double nilaiMat = Convert.ToDouble(mat.Text);
			double nilaiFis = Convert.ToDouble(fis.Text);
			double nilaiKim = Convert.ToDouble(kim.Text);
			double nilaiBio = Convert.ToDouble(bio.Text);
			double jumlahTka = (nilaiMat + nilaiFis + nilaiKim + nilaiBio)/4;
			rataTka.Text = Convert.ToString(jumlahTka);
			
			//TPS
			double nilaiPU = Convert.ToDouble(pu.Text);
			double nilaiPK = Convert.ToDouble(pk.Text);
			double nilaiPBM = Convert.ToDouble(pbm.Text);
			double nilaiPPU = Convert.ToDouble(ppu.Text);
			double jumlahTps = (nilaiPU + nilaiPK + nilaiPBM + nilaiPPU)/4;
			rataTps.Text = Convert.ToString(jumlahTps);
			
			//hasilRata-Rata Kedua Nilai
			double rataUtbk = (jumlahTps + jumlahTka)/2;
			
			nilaiUtbk.Text = Convert.ToString(rataUtbk);
		}
		
		void InsertbtnClick(object sender, EventArgs e)
		{
			InsertData();
		}
		void UpdatebtnClick(object sender, EventArgs e)
		{
			UpdateData();
		}
		
		void DeletebtnClick(object sender, EventArgs e)
		{
			DeleteData();
		}
		
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			CariData();
		}
	}
}
